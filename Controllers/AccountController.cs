using ChatThreeRole.Models;
using ChatThreeRole.Service;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
#nullable disable
public class AccountController: ControllerBase{
    private readonly AccountService _service;
    public AccountController(AccountService service){
        _service = service;
    }
    [HttpGet("{name}")]
    public async Task<string> GetEmail(string name){
        return await _service.GetFirstByName(name);
    }
    
    [HttpGet("GetNameByEmail/{email}")]
    public async Task<string> GetName(string email){
        try{
            return await _service.GetNameByEmail(email);
        }
        catch{
            return null;
        }
    }

    [HttpGet("GetAccountByEmail")]
    public async Task<AccountModel> GetAccountByEmail(string email){
        var account = await _service.GetAccountByEmail(email);
        return account;
    }
    public async Task<IEnumerable<AccountModel>> GetAllAccount(){
        return await _service.GetAllAccount();
    }
    [HttpPost("AddGroupToAccount")]
    public async Task<IActionResult> AddGroupToAccount(UpsertGroupDTO model){
        if(ModelState.IsValid)
        {
            var rs = await _service.AddGroupToAccount(model.GroupId, model.UserEmail);
            if(rs)
                return Ok("Add group successfully");
        }
        return BadRequest();
    }
}