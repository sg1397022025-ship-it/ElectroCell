using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroCell.Entity
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        public int StateId { get; set; }

        [ForeignKey(nameof(StateId))]
        public State State { get; set; }
    }
}