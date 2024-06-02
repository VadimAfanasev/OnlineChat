using System.Text.Json;
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
        _cache = cache;
    }
    
    public async Task JoinChat(UserConnection connection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, connection.chatRoom);

        var stringConnection = JsonSerializer.Serialize(connection);
        
        await _cache.SetStringAsync(Context.ConnectionId, stringConnection);
        
        await Clients
            .Group(connection.chatRoom)
            .ReceiveMessage("Admin", $"{connection.userName} присоединился к чату");
    }

    public async Task SendMessage(string message)
    {
        var stringConnection = await _cache.GetAsync(Context.ConnectionId);
       
        var connection = JsonSerializer.Deserialize<UserConnection>(stringConnection);

        if (connection is not null)
        {
            await Clients
                .Group(connection.chatRoom)
                .ReceiveMessage(connection.userName, message);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var stringConnection = await _cache.GetAsync(Context.ConnectionId);
        var connection = JsonSerializer.Deserialize<UserConnection>(stringConnection);

        if (connection is not null)
        {
            await _cache.RemoveAsync(Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.chatRoom);

            await Clients
                .Group(connection.chatRoom)
                .ReceiveMessage("Admin", $"{connection.userName} присоединился к чату");
        }
        
        await base.OnDisconnectedAsync(exception);
    }
}