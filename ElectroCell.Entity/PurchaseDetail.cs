using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroCell.Entity
{
    public class PurchaseDetail
    {
        [Key]
        public int PurchaseDetailId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal Subtotal { get; set; }

        public int PurchaseId { get; set; }

        [ForeignKey(nameof(PurchaseId))]
        public Purchase Purchase { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public int PresentationId { get; set; }

        [ForeignKey(nameof(PresentationId))]
        public Presentation Presentation { get; set; }
    }
}
