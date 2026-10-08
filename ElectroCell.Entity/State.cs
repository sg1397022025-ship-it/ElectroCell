using System.ComponentModel.DataAnnotations;

namespace ElectroCell.Entity
{
    public class State
    {
        [Key]
        public int StateId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }
    }
}