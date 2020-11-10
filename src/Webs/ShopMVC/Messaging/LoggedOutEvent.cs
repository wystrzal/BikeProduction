namespace Common.Application.Messaging
{
    public class LoggedOutEvent
    {
        public string SessionId { get; set; }

        public LoggedOutEvent(string sessionId)
        {
            SessionId = sessionId;
        }
    }
}
