using System.ComponentModel.DataAnnotations.Schema;

namespace Invoices.Models
{
    [Table("invoices")]
    public class Invoice
    {
        [Column("id_invoice")]
        public int Id { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("state")]
        public int State { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }
    }
}