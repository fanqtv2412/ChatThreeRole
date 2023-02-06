using ChatThreeRole.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;


[Route("api/[controller]")]
[ApiController]
public class MessageController: ControllerBase{
    public readonly MessageService _service;
    public MessageController(MessageService service){
        _service = service;
    }
    [HttpGet]
    public async Task<IEnumerable<MessageModel>> GetAllMessage(){
        return await _service.GetAllMessage();
    }
    [HttpGet("{id:int}/{scroll:int}")]
    public async Task<IEnumerable<MessageModel>> GetMessageByGroupID(int id, int scroll = 0){
        return await _service.GetMessageByGroupID(id, scroll);
    }
    [HttpPost]
    public async Task<MessageModel> CreateMessage(MessageModel message){
        return await _service.SaveMessage(message);
    }
    [HttpGet("lastmessage")]
    public MessageModel GetLastMessage([FromQuery]int id){
        return _service.GetLastMessage(id);
    }
}