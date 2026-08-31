using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models;

public class Ticket
{
    public int Id {get;set;}
    [Required]
    public string Title {get;set;}
    [Required]
    public string Message {get;set;}
    public User User {get;set;}
    public List<int> UserList {get;set;}
    public bool Open {get;set;} = true;
    public DateTime CreationDate {get;set;}
    public DateTime ClosedDate {get;set;}
    public List<Message> Exchange {get;set;} = [];
    public Ticket(User User, string Title = "", string Message = "")
    {
        this.Title = Title;
        this.Message = Message;
        this.User = User;
        this.CreationDate = DateTime.Now;
    }
    public void AddMessage(User User, string Text)
    {
        Message Message = new Message(User,Text);
        this.Exchange.Add(Message);
    }

}


