using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pay.Models
{
    [Table("operations")]
    public class Operation
    {
        [Column("id_operation")]
        public int Id { get; set; }

        [Column("id_invoice")]
        public int InvoiceId { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }
    }
}
