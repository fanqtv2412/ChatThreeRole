using System.Reflection.Metadata.Ecma335;
using ChatThreeRole.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace ChatThreeRole.Service;
#nullable disable
public class AccountService
{
    private readonly MyDBContext _context;
    private readonly ILogger<AccountService> _logger;

    public AccountService(MyDBContext context, ILogger<AccountService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<string> GetFirstByName(string name)
    {
        var account = _context.Account.FirstOrDefault(n => n.FullName == name);
        return account.Email;
    }
    public async Task<IEnumerable<AccountModel>> GetAllAccount()
    {
        return await _context.Account.ToListAsync();
    }

    public async Task<AccountModel> GetAccountByEmail(string email)
    {
        try
        {
            var account = await _context.Account.Include(g => g.Groups).Where(g => g.Email == email).FirstOrDefaultAsync();
            if (account != null)
            {
                foreach (var acc in account.Groups)
                {
                    acc.Accounts = new List<AccountModel>();
                }
                return account;
            }
            else return null;
        }
        catch
        {
            return null;
        }

    }

    public async Task<string> GetNameByEmail(string email)
    {
        var account = await _context.Account.FindAsync(email.Trim());
        if (account != null)
            return account.FullName;
        else return null;
    }

    public async Task<bool> AddGroupToAccount(int groupId, string email)
    {
        try
        {
            var group = await _context.Group.FindAsync(groupId);
            var acc = await _context.Account.Include(a => a.Groups)
            .FirstOrDefaultAsync(a => a.Email.Equals(email.Trim()));
            if (group != null && acc != null)
            {
                acc.Groups.Add(group);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        return false;
    }


}