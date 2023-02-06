using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Options;

namespace ChatThreeRole.Models;
public class GroupModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public string GroupName { get; set; }
    public string? ImageOfGroup{get; set;}
    public ICollection<MessageModel>? MessageModels { get; set; }

    public ICollection<AccountModel>? Accounts{get; set;}
}