using System.ComponentModel.DataAnnotations;

namespace Week08_1_SignalREx.Models
{
    public class ChatMessage
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
