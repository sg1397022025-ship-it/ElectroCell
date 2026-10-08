using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroCell.Entity
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public decimal CashReceived { get; set; }

        [Required]
        public decimal Change { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
