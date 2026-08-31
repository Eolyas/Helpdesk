using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelpDesk.Pages;

public class ViewTicketModel : PageModel
{
    public Ticket? Ticket { get; set; }
    [BindProperty]
    public string Message {get;set;} = "";

    public IActionResult OnGet(int Id)
    {
        Ticket = ListData.Tickets.FirstOrDefault(t => t.Id == Id,ListData.Tickets[0]);
        return Page();
    }
    public IActionResult OnPostSendMessage(int TicketId)
    {
        Ticket = ListData.Tickets.FirstOrDefault(t => t.Id == TicketId,ListData.Tickets[0]);
        Ticket?.AddMessage(1,Message);
        return LocalRedirect($"/ticket/{TicketId}");
    }
}