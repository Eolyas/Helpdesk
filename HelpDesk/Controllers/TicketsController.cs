using Microsoft.AspNetCore.Mvc;
using HelpDesk.Models;

namespace HelpDesk.Controllers;

public class TicketsController : Controller
{
    // POST /Tickets/Submit
    [HttpPost]
    public IActionResult Submit(string Title, string Text, User User)
    {
        ListData.Tickets.Add(new Ticket(User, Title, Text));
        return Redirect("/");
    }
}