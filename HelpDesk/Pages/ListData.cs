using HelpDesk.Models;
public class ListData
{
    public static List<Ticket> Tickets {get;set;} = new();
    public static List<User> Users {get;set;} = new();
    public Ticket FindTicketById(int Id)
    {
        var ticket = Tickets.FirstOrDefault(t => t.Id == Id,Tickets[0]);
        return ticket;
    }
}