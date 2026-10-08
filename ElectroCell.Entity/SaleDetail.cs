using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroCell.Entity
{
    public class SaleDetail
    {
        [Key]
        public int SaleDetailId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Subtotal { get; set; }

        public int SaleId { get; set; }

        [ForeignKey(nameof(SaleId))]
        public Sale Sale { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
