public class User
{
    private static int global_id = 0;
    public int id = global_id++;
    public string firstname;
    public string lastname;
    public string email;
    public User(string firstname, string lastname, string email)
    {
        this.firstname = firstname;
        this.lastname = lastname;
        this.email = email;
    }
}