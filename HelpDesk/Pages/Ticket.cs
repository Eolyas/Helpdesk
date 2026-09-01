using System.ComponentModel.DataAnnotations;
using HelpDesk.Data;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Models;

public class Ticket
{
    public int Id {get;set;}
    [Required]
    public string Title {get;set;}
    [Required]
    public string Text {get;set;}
    [Required]
    public int UserId {get;set;}
    public User User {get;set;} = null!;
    public List<int> UserList {get;set;} = [];
    public bool Open {get;set;} = true;
    public DateTime CreationDate {get;set;}
    public DateTime ClosedDate {get;set;}
    public List<Message> Exchange {get;set;} = [];
    public Ticket(int UserId, string Title = "", string Text = "")
    {
        this.Title = Title;
        this.Text = Text;
        this.UserId = UserId;
        this.CreationDate = DateTime.Now;
        UserList.Add(UserId);
    }
    public void AddMessage(int UserId, string Text)
    {
        Message Message = new Message(UserId, Id, Text);
        this.Exchange.Add(Message);
    }

}


