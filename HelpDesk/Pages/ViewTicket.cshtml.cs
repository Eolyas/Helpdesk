using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HelpDesk.Models;

namespace HelpDesk.Pages;

public class ViewTicketModel : PageModel
{
    public Ticket? Ticket { get; set; }
    [BindProperty]
    public string Text {get;set;} = string.Empty;

    public IActionResult OnGet(int Id)
    {
        Ticket = ListData.Tickets.FirstOrDefault(t => t.Id == Id,ListData.Tickets[0]);
        return Page();
    }
    public IActionResult OnPostSendMessage(Ticket Ticket, User User)
    {
        Ticket = ListData.Tickets.FirstOrDefault(t => t.Id == Ticket.Id,ListData.Tickets[0]);
        Ticket?.AddMessage(User,Text);
        return LocalRedirect($"/ticket/{Ticket.Id}");
    }
}