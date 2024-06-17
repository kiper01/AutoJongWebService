using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoJongWebService
{
    [Table("Admins")]
    public class AdminItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [EnumDataType(typeof(RoleType))]
        public RoleType Role { get; set; }

        public AdminItem()
        {
            Id = Guid.NewGuid();
        }
        public enum RoleType
        {
            SuperAdmin,
            Admin
        }
    }
}
