using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroCell.Entity
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [StringLength(9)]
        public string Phone { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(80)]
        public string Email { get; set; }

        public int StateId { get; set; }

        [ForeignKey(nameof(StateId))]
        public State State { get; set; }
    }
}
