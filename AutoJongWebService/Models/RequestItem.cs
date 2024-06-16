using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static AutoJongWebService.Models.CarItem;

namespace AutoJongWebService.Models
{
    [Table("Request")]
    public class RequestItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(12)]
        [Phone]
        public string Number { get; set; }

        [MaxLength(255)]
        public string Model { get; set; }

        [Required]
        [EnumDataType(typeof(StatusType))]
        public StatusType Status { get; set; }

        public RequestItem()
        {
            Id = Guid.NewGuid();
        }

        public enum StatusType
        {
            Waiting,
            CallBack,
            InWork,
            Done
        }
    }
}
