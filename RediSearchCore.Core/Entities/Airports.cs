namespace RediSearchCore.Core.Entities
{
    public class Airports : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
