using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HelpDesk.Models;

public class Message
{
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public int UserId {get;set;}
    [Required]
    public string Text {get;set;} = string.Empty;
    public int TicketId {get;set;}
    public Message(int UserId, string Text)
    {
        this.UserId = UserId;
        this.Text = Text;
    }
}