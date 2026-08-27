using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelpDesk.Pages;

public class ViewTicketModel : PageModel
{
    public Ticket? Ticket { get; set; }

    public IActionResult OnGet(int id)
    {
        Ticket = ListData.Tickets.FirstOrDefault(t => t.id == id);
        return Page();
    }
}