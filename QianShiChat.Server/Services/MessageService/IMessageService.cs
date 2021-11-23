using System.Threading.Tasks;

using QianShiChat.Server.Models.Entities;

namespace QianShiChat.Server.Services.MessageService
{
    public interface IMessageService
    {
        Task AddMessage(string content, ContentType type, int toUserId, bool isSend = false);
        Task ReadMessage(long id, bool isSend = false);
        Task DelMessage(long id, bool isSend = false);
    }
}
