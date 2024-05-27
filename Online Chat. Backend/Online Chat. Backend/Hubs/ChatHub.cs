using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Online_Chat._Backend.Models;

namespace Online_Chat._Backend.Hubs;

public interface IChatClient
{
    public Task ReceiveMessage(string userName, string message);
}

public class ChatHub: Hub<IChatClient>
{
    private readonly IDistributedCache _cache;
    public ChatHub(IDistributedCache cache)
    {
        _cache - cache;
    }
    
    public async Task JoinChat(UserConnection connection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, connection.chatRoom);

        await Clients
            .Group(connection.chatRoom)
            .ReceiveMessage("Admin", $"{connection.userName} присоединился к чату");
    }

    public async Task SendMessage(string message)
    {
        await Clients
            .Group(Connection.ChatRoom)
            .ReceiveMessage("Admin", $"{connection.userName} присоединился к чату");
    }
}