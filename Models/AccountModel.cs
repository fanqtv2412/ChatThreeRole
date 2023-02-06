using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ChatThreeRole.Models;
public class AccountModel{
    [Key]
    [Display(Name ="Email")]
    public string Email{get; set;}
    [Display(Name ="Full Name")]
     [MaxLength(100)]
    public string FullName {get; set;}
    [Display(Name = "Password")]
     [MaxLength(20)]
    public string Password {get; set;}
    public string? Avatar {get; set;}

    public ICollection<GroupModel>? Groups {get; set;}
}