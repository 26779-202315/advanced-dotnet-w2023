using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using Week08_1_SignalREx.Models;

namespace Week08_1_SignalREx
{
    public class ChatHub:Hub
    {
        private readonly AppDbContext _dbContext;
        private static ConcurrentDictionary<string, ChatRoom> chatRooms = new ConcurrentDictionary<string, ChatRoom>();
        private static ConcurrentDictionary<string, string> userNames = new ConcurrentDictionary<string, string>();

        public ChatHub(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
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

        public async Task JoinRoom(string roomName)
        {
            roomName = roomName.ToLower();
            string currentConnectionId = Context.ConnectionId;

            if (!chatRooms.ContainsKey(roomName))
            {
                ChatRoom newRoom = new ChatRoom()
                {
                    RoomName = roomName,
                    ConnectionIds = new List<string>()
                };
                newRoom.ConnectionIds.Add(currentConnectionId);
                chatRooms.TryAdd(roomName, newRoom);

            }
            else
            {
                ChatRoom existingRoom = new ChatRoom();
                chatRooms.TryGetValue(roomName, out existingRoom);
                existingRoom.ConnectionIds.Add(currentConnectionId);

                chatRooms.TryAdd(roomName, existingRoom);
            }

            await Groups.AddToGroupAsync(currentConnectionId, roomName);
            await Clients.Caller.SendAsync("ReceiveMessage", "Chat Hub", DateTimeOffset.Now, $"You joined room: {roomName}!");
        }

        public async Task SendMessage(string roomname, string userName, string textMessage)
        {
            if (!userNames.ContainsKey(Context.ConnectionId))
            {
                userNames.TryAdd(Context.ConnectionId, userName);
            }


            var message = new ChatMessage
            {
                UserName = userName,
                Message = textMessage,
                TimeStamp = DateTimeOffset.Now
            };

            //Add Message for DB

            await Clients.Group(roomname.ToLower()).SendAsync("ReceiveMessage", message.UserName, message.TimeStamp, message.Message);
        }
    }
}
