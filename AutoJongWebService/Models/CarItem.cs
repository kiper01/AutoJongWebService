using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoJongWebService.Models
{
    [Table("Cars")]
    public class CarItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        [Range(1970, 2024)]
        public uint StartYear { get; set; }

        [Required]
        [Range(0, 100000000)] // 100M
        public uint StartPrice { get; set; }

        [Required]
        [Range(0, 100000000)]
        public uint EndPrice { get; set; }

        [Required]
        public FuelType Fuel { get; set; }

        [Required]
        [Range(0, 6.0)]
        public double StartEngineVolume { get; set; }

        [Required]
        public GearboxType Gearbox { get; set; }

        [Required]
        public CountryType Country { get; set; }

        public CarItem(string name)
        {
            Name = name;
            Image = Name + ".jpeg";
        }

        public enum FuelType
        {
            Petrol,
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
