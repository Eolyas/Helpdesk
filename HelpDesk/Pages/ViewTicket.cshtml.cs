using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HelpDesk.Models;
using HelpDesk.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace HelpDesk.Pages;

public class ViewTicketModel : PageModel
{
    private readonly HelpDeskDbContext database;
    public ViewTicketModel(HelpDeskDbContext database)
    {
        this.database = database;
    }
    public Ticket? Ticket { get; set; }
    [BindProperty]
    public string Text {get;set;} = string.Empty;

    public async Task<IActionResult> OnGetAsync(int Id)
    {
        Ticket = await database.Tickets
        .Include(Ticket => Ticket.User)
        .Include(Ticket => Ticket.Exchange)
        .SingleOrDefaultAsync(Ticket => Ticket.TicketId == Id);
        if (Ticket == null)
        {
            return NotFound();
        }
        return Page();
    }
    public async Task<IActionResult> OnPostSendMessage(Ticket Ticket, int UserId)
    {
        Ticket = ListData.Tickets.FirstOrDefault(t => t.TicketId == Ticket.TicketId,ListData.Tickets[0]);
        Ticket?.AddMessage(UserId,Text);
        return LocalRedirect($"/ticket/{Ticket!.TicketId}");
    }
}