using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoJongWebService.Models
{
    [Table("Reviews")]
    public class ReviewItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Login { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        public ReviewItem()
        {
            Id = Guid.NewGuid();
        }
    }
}
