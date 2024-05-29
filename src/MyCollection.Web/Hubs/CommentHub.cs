using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace MyCollection.Web.Hubs;

public class CommentHub : Hub
{
    public async Task JoinToGroup(string userId,string groupId)
    {
        await Groups.AddToGroupAsync(userId, groupId);
    }

    public async Task SendComment(string userId,string imgUrl,string groupId, string message)
    {
        await Clients.Group(groupId).SendAsync("RecieveMessage",userId, imgUrl, message);
    }
}
