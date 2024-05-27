namespace AutoJongWebService.Models
{
    public class Test
    {
        public uint StartYear { get; set; }
        public uint StartPrice { get; set; }
        public uint EndPrice { get; set; }
        public FuelType Fuel { get; set; }
        public double StartEngineVolume { get; set; }
        public GearboxType Gearbox { get; set; }
        public CountryType Country { get; set; }

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