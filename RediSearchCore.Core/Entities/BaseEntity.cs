namespace RediSearchCore.Core.Entities
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }

        #region Cache Attributes
        private double _score = 1;
        public double Score
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
        #endregion
    }
}
