using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks.Dataflow;
using ChatThreeRole.Models;
using Microsoft.EntityFrameworkCore;
namespace ChatThreeRole.Models;
public class GroupService{
    private readonly MyDBContext _context;
    public GroupService(MyDBContext context){
        _context = context;
    }
    public async Task<IEnumerable<GroupModel>> getAll(){
        var codeStock = await _context.Group.ToListAsync();
        return codeStock;
    }
    public async Task<GroupModel> AddGroup(GroupModel group){
        try{
            await _context.AddAsync(group);
            await _context.SaveChangesAsync();
            return group;
        }
        catch{
            return null;
        }
    }
    public async Task<GroupModel> GetNameGroup(string name){
        var group = await _context.Group.FirstOrDefaultAsync(m=>m.GroupName == name.Trim());
        return group;
    }
    public string GetNameGroupByID(int id){
        var group = _context.Group.Find(id);
        return group.GroupName;
    }

    public async Task<GroupModel> SelectById(int id)
    {
        var group = await _context.Group.Include(g => g.Accounts).Where(g => g.Id == id).FirstOrDefaultAsync();
        foreach(var acc in group.Accounts)
        {
            acc.Groups = new List<GroupModel>();
        }
        return group;
    }

    public string GetImageOfGroup(int id, string email = null )
    {
        var group = _context.Group.Find(id);
        if(group == null) return null;
        else
        {
            if (group.GroupName.Contains(email) && group.GroupName.Contains(":"))
            {
                var account = _context.Account.Find(GetAnotherAccount(group.GroupName, email));
                if (account != null) return account.Avatar;
                else return null;
            }
            return group.ImageOfGroup;
        }
    }

    private string GetAnotherAccount(string groupName, string email)
    {
        var twoEmail = groupName.Split(":");
        var result = email == twoEmail[0] ? twoEmail[1] : twoEmail[0];
        return result;
    }
    public string RegisterAccountToGroup(string email, int groupID){
        
        return "";
    }
}