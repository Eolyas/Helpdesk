using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HelpDesk.Models;

public class Message
{
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public User User {get;set;}
    [Required]
    public string Text {get;set;} = string.Empty;
    public Ticket Ticket {get;set;}
    public Message(User User, string Text)
    {
        this.User = User;
        this.Text = Text;
    }
}