using System.Text.RegularExpressions;

namespace ChatThreeRole.Models;

public class MessageModel
{
    public long? Id { get; set; }
    public string Messsage { get; set; }

    public int? GroupID { get; set; }
    public GroupModel? Group { get; set; }

    public DateTime? DateOfMsg{get; set;}

}