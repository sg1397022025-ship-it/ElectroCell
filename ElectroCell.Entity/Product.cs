using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroCell.Entity
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [StringLength(40)]
        public string Model { get; set; }

        [Required]
        public decimal PurchasePrice { get; set; }

        [Required]
        public decimal SalePrice { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public int BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; }

        public int PresentationId { get; set; }

        [ForeignKey(nameof(PresentationId))]
        public Presentation Presentation { get; set; }

        public int StateId { get; set; }

        [ForeignKey(nameof(StateId))]
        public State State { get; set; }
    }
}