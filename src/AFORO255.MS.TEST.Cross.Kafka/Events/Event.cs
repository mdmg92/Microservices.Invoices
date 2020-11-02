﻿using System;

 namespace AFORO255.MS.TEST.Cross.Kafka.Events
{
    public class Event
    {
        public DateTime Timestamp { get; protected set; }

        public Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
