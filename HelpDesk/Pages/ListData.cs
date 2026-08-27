public class ListData
{
    public static List<Ticket> Tickets {get;set;} = new();
    public static List<User> Users {get;set;} = new();
    public Ticket FindTicketById(int id)
    {
        var ticket = Tickets.FirstOrDefault(t => t.id == id,Tickets[0]);
        return ticket;
    }
}