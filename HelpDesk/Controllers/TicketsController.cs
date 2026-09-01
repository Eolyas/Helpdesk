using Microsoft.AspNetCore.Mvc;
using HelpDesk.Models;

namespace HelpDesk.Controllers;

public class TicketsController : Controller
{
    // POST /Tickets/Submit
    [HttpPost]
    public IActionResult Submit(string Title, string Text, int UserId)
    {
        ListData.Tickets.Add(new Ticket(UserId, Title, Text));
        return Redirect("/");
    }
}