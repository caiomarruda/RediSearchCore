namespace RediSearchCore.Core.Entities
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }

        #region Cache Attributes
        public double Score { get; set; } = 0;
        public string Tag { get; set; }
        #endregion
    }
}
