using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HelpDesk.Models;

public class Message
{
    public int MessageId {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    [Required]
    public string Text {get;set;} = string.Empty;
    public int TicketId {get;set;}
    public Ticket Ticket {get;set;} = null!;
    [Required]
    public int UserId {get;set;}
    public User User {get;set;} = null!;
    public Message(int UserId, int TicketId, string Text)
    {
        this.UserId = UserId;
        this.Text = Text;
        this.TicketId = TicketId;
    }
}