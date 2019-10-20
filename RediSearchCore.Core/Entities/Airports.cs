namespace RediSearchCore.Core.Entities
{
    public class Airports : BaseEntity
    {
        private int _score;
        public string Code { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                if (value > 0 && value <= 5)
                    _score = value;
            }
        }
        public string[] Tags { get; set; }
    }
}
