using System.Collections.Immutable;
using ChatThreeRole.Models;
using Microsoft.EntityFrameworkCore;

public class MessageService{
    private readonly MyDBContext _context;
    public MessageService(MyDBContext context){
        _context = context;
    }
    public async Task<IEnumerable<MessageModel>> GetAllMessage(){
        return await _context.Messages.ToListAsync();
    }
    public async Task<IEnumerable<MessageModel>> GetMessageByGroupID(int id, int scroll = 0){
        var listMessage = from msg in _context.Messages where msg.GroupID == id select msg;
        if(15 + scroll * 5 > listMessage.Count())   
            return await listMessage.ToListAsync();
            
        var list = await listMessage.ToListAsync();
        var res = new List<MessageModel>();
        for(int i = list.Count() - (15 + scroll * 5); i< list.Count(); i++){
            res.Add(list[i]);
        }
        return res;
    }
    public async Task<MessageModel> SaveMessage(MessageModel message){
        try{
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }
        catch {
            return null;
        }
    }
    public MessageModel GetLastMessage(int id){
        var res = _context.Messages.OrderByDescending(x => x.DateOfMsg).FirstOrDefault(x => x.GroupID == id);
        if(res == null) return null;
        else
            return res;
    }
    
}