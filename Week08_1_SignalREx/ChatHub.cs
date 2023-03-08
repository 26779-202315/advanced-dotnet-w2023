using Microsoft.AspNetCore.SignalR;
using Week08_1_SignalREx.Models;

namespace Week08_1_SignalREx
{
    public class ChatHub:Hub
    {
        public async Task SendAllMessage(string userName, string textMessage)
        {
            var message = new ChatMessage
            {
                UserName = userName,
                Message = textMessage,
                TimeStamp = DateTimeOffset.Now
            };

            await Clients.All.SendAsync("ReceiveMessage", message.UserName, message.TimeStamp, message.Message);

        }

    }
}
