using System.Security.Cryptography.X509Certificates;
using ChatThreeRole.Hubs;
using ChatThreeRole.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.SignalR;

[Route("api/[controller]")]
[ApiController]
public class GroupController: ControllerBase{
    private readonly GroupService _service;
    private readonly IHubContext<ChatHub> _contextHub;
    public GroupController(GroupService service, IHubContext<ChatHub> contextHub)
    {
        _service = service;
        _contextHub = contextHub;
    }

    [HttpGet]
    public async Task<IEnumerable<GroupModel>> GetAllGroup(){
        return await _service.getAll();
    }

    [HttpPost]
    public async Task<GroupModel> PostGroup(GroupModel group){
        return await _service.AddGroup(group);
    }
    
    [HttpGet("{name}")]
    public async Task<GroupModel> GetGroupByName(string name){
        return await _service.GetNameGroup(name);
    }

    [HttpGet("GetNameByID")]
    public string GetNameByID([FromQuery]int id){
        return _service.GetNameGroupByID(id);
    }
    
    [HttpGet("GetImage")]
    public string GetImageOfGroup([FromQuery] int id){
        return _service.GetImageOfGroup(id);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var group = await _service.SelectById(id);
        return Ok(group);
    }
    [HttpGet("GetThroughHub")]
    public async Task<List<string>> GetAllNameGroupThroughHub()
    {
        return ChatHub._listNameGroup;
    }
    
}