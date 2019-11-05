namespace RediSearchCore.Core.Entities
{
    public class FastFoods : BaseEntity
    {
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
    }
}
