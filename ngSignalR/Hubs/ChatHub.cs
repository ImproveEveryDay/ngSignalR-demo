using Microsoft.AspNet.SignalR;

namespace AngularSignal.Hubs
{
    public class ChatHub: Hub
    {
        public void SendMessage(ChatMessage cm)
        {
            Clients.Others.addMessage(cm.Username, cm.Message);
        }

        public class ChatMessage
        {
            public string Username { get; set; }
            public string Message { get; set; }
        }
    }
}