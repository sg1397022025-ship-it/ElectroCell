using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroCell.Entity
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Total { get; set; }

        public int SupplierId { get; set; }

        [ForeignKey(nameof(SupplierId))]
        public Supplier Supplier { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}