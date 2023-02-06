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

    public string GetImageOfGroup(int id){
        var group = _context.Group.Find(id);
        if(group == null) return null;
        else
            return group.ImageOfGroup;
    }

    public string RegisterAccountToGroup(string email, int groupID){
        
        return "";
    }
}