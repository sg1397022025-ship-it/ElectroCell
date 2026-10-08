using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroCell.Entity
{
    public class Presentation
    {
        [Key]
        public int PresentationId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        public int Units { get; set; }

        public int StateId { get; set; }

        [ForeignKey(nameof(StateId))]
        public State State { get; set; }
    }
}
