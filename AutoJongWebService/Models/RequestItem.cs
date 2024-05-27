using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoJongWebService.Models
{
    [Table("Request")]
    public class RequestItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(12)]
        [Phone]
        public string Number { get; set; }

        [MaxLength(255)]
        public string Model { get; set; }
    }
}
