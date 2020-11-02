using System;
using System.Threading;
using System.Threading.Tasks;
using AFORO255.MS.TEST.Cross.Kafka.Bus;
using AFORO255.MS.TEST.Cross.Kafka.Events;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AFORO255.MS.TEST.Cross.Kafka
{
    public partial class KafkaBus
    {
        public class KafkaMessageConsumer<T> : IHostedService, IDisposable
            where T : Event, new()
        {
            private readonly IEventBus _bus;
            string topic;
            ConsumerConfig consumerConfig;
            IConsumer<string, T> kafkaConsumer;
            Thread pollThread;
            CancellationTokenSource cancellationTokenSource;

            public KafkaMessageConsumer(IConfiguration config, IEventBus bus)
            {
                _bus = bus;
                consumerConfig = new ConsumerConfig();
                config.GetSection("Kafka:ConsumerSettings").Bind(consumerConfig);
                topic = typeof(T).Name;
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {
                cancellationTokenSource = new CancellationTokenSource();
                kafkaConsumer = new ConsumerBuilder<string, T>(consumerConfig)
                    .SetValueDeserializer(new JsonDeserializer<T>().AsSyncOverAsync())
                    .Build();
                pollThread = new Thread(consumerLoop);
                pollThread.Start();

                return Task.CompletedTask;
            }

            public async Task StopAsync(CancellationToken cancellationToken)
            {
                cancellationTokenSource.Cancel();

                // Async methods should never block, so block waiting for the poll loop to finish on another
                // thread and await completion of that.
                await Task.Run(() => { pollThread.Join(); }, cancellationToken);;
            }

            public void Dispose()
            {
                kafkaConsumer.Close(); // Commit offsets and leave the group cleanly.
                kafkaConsumer.Dispose();
            }

            private void consumerLoop()
            {
                kafkaConsumer.Subscribe(topic);
                while (true)
                {
                    try
                    {
                        var result = kafkaConsumer.Consume(cancellationTokenSource.Token);

                        // Handle message...
                        _bus.HandleMessage(result.Message.Key, result.Message.Value).Wait();
                    }
                    catch (TaskCanceledException)
                    {
                        // StopAsync called.
                        break;
                    }
                    catch (ConsumeException e)
                    {
                        // Consumer errors should generally be ignored (or logged) unless fatal.
                        Console.WriteLine($"Consume error: {e.Error.Reason}");

                        if (e.Error.IsFatal)
                        {
                            // https://github.com/edenhill/librdkafka/blob/master/INTRODUCTION.md#fatal-consumer-errors
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Unexpected error: {e}");
                        break;
                    }
                }
            }
        }
    }
}