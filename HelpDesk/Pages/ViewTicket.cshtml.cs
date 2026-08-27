using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelpDesk.Pages;

public class ViewTicketModel : PageModel
{
    public Ticket? Ticket { get; set; }
    [BindProperty]
    public string Message {get;set;} = "";

    public IActionResult OnGet(int id)
    {
        Ticket = ListData.Tickets.FirstOrDefault(t => t.id == id,ListData.Tickets[0]);
        return Page();
    }
    public IActionResult OnPostSendMessage(int ticket_id)
    {
        Ticket = ListData.Tickets.FirstOrDefault(t => t.id == ticket_id,ListData.Tickets[0]);
        Ticket?.AddMessage(1,Message);
        return LocalRedirect($"/ticket/{ticket_id}");
    }
}