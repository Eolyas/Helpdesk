public class Ticket
{
    private static int global_id = 0;
    public int id = global_id++;
    public string title;
    public string message;
    public int user_id;
    public bool open = true;
    public List<(int, string)> exchange = [];
    public Ticket(int user_id = 0, string title = "", string message = "")
    {
        this.title = title;
        this.message = message;
        this.user_id = user_id;
    }
    public void AddMessage(int id, string message)
    {
        this.exchange.Add((id,message));
    }

}


