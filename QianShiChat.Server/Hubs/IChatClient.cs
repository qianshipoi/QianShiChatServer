using QianShiChat.Server.Models.ViewModels;

namespace QianShiChat.Server.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(int userId, MessageViewModel message);

    }
}
