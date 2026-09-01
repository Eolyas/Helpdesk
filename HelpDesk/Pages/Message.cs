using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HelpDesk.Models;

public class Message
{
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    [Required]
    public int UserId {get;set;}
    [Required]
    public User User {get;set;}
    [Required]
    public string Text {get;set;} = string.Empty;
    public int TicketId {get;set;}
    [Required]
    public Ticket Ticket {get;set;}
    public Message(int UserId, int TicketId, string Text)
    {
        this.UserId = UserId;
        this.Text = Text;
        this.TicketId = TicketId;
    }
}