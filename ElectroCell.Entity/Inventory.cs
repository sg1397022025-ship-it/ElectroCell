using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroCell.Entity
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int CurrentStock { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public int MovementTypeId { get; set; }

        [ForeignKey(nameof(MovementTypeId))]
        public MovementType MovementType { get; set; }
    }
}
