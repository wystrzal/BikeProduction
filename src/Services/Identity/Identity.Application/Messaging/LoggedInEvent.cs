using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Application.Messaging
{
    public class LoggedInEvent
    {
        public string UserId { get; set; }
        public string SessionId { get; set; }

        public LoggedInEvent(string userId, string sessionId)
        {
            UserId = userId;
            SessionId = sessionId;
        }
    }
}
