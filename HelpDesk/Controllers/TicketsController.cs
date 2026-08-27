using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers;

public class TicketsController : Controller
{
    // POST /Tickets/Submit
    [HttpPost]
    public IActionResult Submit(string title, string message, int user_id)
    {
        ListData.Tickets.Add(new Ticket(user_id, title, message));
        return Redirect("/");
    }
}