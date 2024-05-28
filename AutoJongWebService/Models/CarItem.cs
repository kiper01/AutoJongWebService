using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoJongWebService.Models
{
    [Table("Cars")]
    public class CarItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Range(1970, 2024)]
        public uint Year { get; set; }

        [Required]
        [Range(0, 100000000)]
        public uint Price { get; set; }

        [Required]
        [EnumDataType(typeof(FuelType))]
        public FuelType Fuel { get; set; }

        [Required]
        [Range(0, 6.0)]
        public double EngineVolume { get; set; }

        [Required]
        [EnumDataType(typeof(GearboxType))]
        public GearboxType Gearbox { get; set; }

        [Required]
        [EnumDataType(typeof(CountryType))]
        public CountryType Country { get; set; }

        public CarItem()
        {
            Id = Guid.NewGuid();
        }

        public enum FuelType
        {
            Gasoline,
            Diesel,
            Electric,
            Hybrid
        }

        public enum GearboxType
        {
            Manual,
            Automatic,
            SemiAutomatic
        }

        public enum CountryType
        {
            Japan,
            Korea,
            China
        }
    }
}
