namespace Common.Application.Messaging
{
    public class LoggedInEvent
    {
        public string UserId { get; set; }
        public string SessionId { get; set; }
    }
}
