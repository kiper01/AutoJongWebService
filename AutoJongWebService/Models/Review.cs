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
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Text { get; set; }

        public ReviewItem()
        {
            Id = Guid.NewGuid();
        }
    }
}
