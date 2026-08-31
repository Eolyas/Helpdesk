using System.ComponentModel.DataAnnotations;
namespace HelpDesk.Models;
public class User
{
    public int Id {get;set;}
    [Required]
    public string FirstName {get;set;}
    [Required]
    public string LastName {get;set;}
    [Required]
    [EmailAddress]
    public string Email {get;set;}
    public User(string FirstName, string LastName, string Email)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Email = Email;
    }
}