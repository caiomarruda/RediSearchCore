namespace RediSearchCore.Core.Models
{
    public class FastFoodsInputCommand
    {
        public string Sentence { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Kilometers { get; set; }
    }
}
