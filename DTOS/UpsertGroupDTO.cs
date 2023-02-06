using System.ComponentModel.DataAnnotations;

public class UpsertGroupDTO
{
    public int GroupId { get; set; }
    [Required]
    public string UserEmail { get; set; }
}