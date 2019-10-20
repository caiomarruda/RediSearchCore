namespace RediSearchCore.Core.Models
{
    public class Notification
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public object Errors { get; set; }
    }
}
