using System.Text.RegularExpressions;
using Microsoft.AspNetCore.SignalR;

namespace MainMikitan.Application.Hubs;

public class SystemHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var callIdentifier = httpContext.Request.Query["callIdentifier"];
        await Groups.AddToGroupAsync(Context.ConnectionId, callIdentifier);
        await base.OnConnectedAsync();
    }
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var httpContext = Context.GetHttpContext();
        var sessionIdentifier = httpContext.Request.Query["sessionIdentifier"];
        await base.OnDisconnectedAsync(exception);
    }
    
    public async Task<bool> JoinConnection(string sessionIdentifier)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, sessionIdentifier);
        return true;
    }

    public async Task<bool> CheckConnection(string sessionIdentifier)
    {
        await Clients.Group(sessionIdentifier).SendAsync("CheckConnection");
        return true;
    }
}