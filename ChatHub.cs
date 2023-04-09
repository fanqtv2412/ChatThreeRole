using Microsoft.AspNetCore.SignalR;
using ChatThreeRole.Controllers;
using System.Net;
using Microsoft.Extensions.Localization;

namespace ChatThreeRole.Hubs;

public static class UserHandler{
    public static HashSet<string> ConnectedIDs = new HashSet<string>();
}

public class ChatHub : Hub{
    public List<string> listAccount = new List<string>();
    public readonly static List<string> _listNameGroup = new List<string>();
    public void receiveList(string nameAccount){
        listAccount.Add(nameAccount);
    }

    public async Task SendMessageToGroup(string groupName, string messsage,string groupID){
        await Clients.Group(groupName).SendAsync("RecvFromGroup",groupName, messsage, groupID);
    }
    public async Task SendMessageToAllClients(string messsage){
        await Clients.All.SendAsync("Recv", messsage);
    }
    public async Task InvokeSendALlClient(string messsage){
        await Clients.User("admin").SendAsync("RequireSendToAllCLient",messsage);
    }
    public async Task SendToServer(){
        await Clients.All.SendAsync("TotalConnect", HomeController.listAccount.Count);
    }

    public async Task SendNewGroupToMoniter(string messsage){
        await Clients.All.SendAsync("NewGroup", messsage);
    }
    public async Task CreateGroup(string groupName)
    {
        _listNameGroup.Add(groupName);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        await Clients.All.SendAsync("NewGroupCreated", groupName, Context.ConnectionId);
    }
    public async Task AddToGroup(string groupName, string email)
    {
        if(!_listNameGroup.Contains(groupName))
            _listNameGroup.Add(groupName);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        await Clients.Group(groupName).SendAsync("SendJoin", $"{email} has joined the group.\n" + DateTime.Now.ToString("mm:ss"), groupName);
    }
    public async Task RemoveFromGroup(string groupName, string email)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        await Clients.All.SendAsync("SendLeave", $"{email} has left the group.\n" + DateTime.Now.ToString("mm:ss"), groupName);
    }
    public async Task SendStatus(string name){
        await Clients.All.SendAsync("Status", name);
    }
    public async Task SendPrivateMessage(string user, string messsage){
        await Clients.User(user).SendAsync("PrivateMessage", messsage);
    }

    public async Task SendTotalAccount(string listAcc){
        await Clients.All.SendAsync("TotalAccount", listAcc);
    }
    
}