using Microsoft.AspNetCore.SignalR;
using MyCollection.Application.Interfaces;
using MyCollection.Domain.Entities;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MyCollection.Web.Hubs;

public class CommentHub(ICommentService commentService,IUserService userService) : Hub
{
    static List<string> connections = new List<string>();
    public async Task JoinToGroup(string userId,string groupId)
    {
        connections.Add(Context.ConnectionId);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        Console.WriteLine(JsonSerializer.Serialize(connections));
    }

    public async Task SendComment(string userName,string imgUrl,string groupId, string message,string date)
    {
        var user = await userService.GetByUserNameAsync(userName);
        var newComment = new Comment
        {
            ItemId = Guid.Parse(groupId.Trim()),
            SenderId = user.Id,
            Message = message,
            SendAt = DateTimeOffset.UtcNow
        };

        await commentService.CreateAsync(newComment,saveChanges: true,cancellationToken: Context.ConnectionAborted);

        await Clients.Group(groupId).SendAsync("RecieveMessage",userName, imgUrl, message,DateTime.Parse(date).ToString("dd.MM.yyyy"));
    }
}
