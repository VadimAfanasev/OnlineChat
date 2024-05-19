using Microsoft.AspNetCore.SignalR;
using Online_Chat._Backend.Models;

namespace Online_Chat._Backend.Hubs;

public interface IChatClient
{
    public Task ReceiveMessage(string userName, string message);
}

public class ChatHub: Hub<IChatClient>
{
    public async Task JoinChat(UserConnection connection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, connection.chatRoom);

        await Clients.Group(connection.chatRoom)
            .ReceiveMessage("Admin", $"{connection.userName} присоединился к чату");
    }
}