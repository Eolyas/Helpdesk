using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HelpDesk.Models;
using HelpDesk.Data;
using Microsoft.EntityFrameworkCore;

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
                .ThenInclude(message => message.User)
            .SingleOrDefaultAsync(Ticket => Ticket.TicketId == Id);
        if (Ticket == null)
        {
            return NotFound();
        }
        return Page();
    }
    public async Task<IActionResult> OnPostSendMessageAsync(int TicketId, int UserId)
    {
        Ticket = await database.Tickets.SingleOrDefaultAsync(ticket => ticket.TicketId == TicketId);
        if (Ticket is null)
        {
            return NotFound();
        }
        User? User = await database.Users.SingleOrDefaultAsync(user => user.UserId == UserId);
        if (User is null)
        {
            return BadRequest("User not found");
        }
        if (string.IsNullOrWhiteSpace(Text))
        {
            return LocalRedirect($"/ticket/{TicketId}");
        }
        Message Message = new Message(UserId, TicketId, Text);
        database.TicketMessages.Add(Message);
        int SavedRows = await database.SaveChangesAsync();
        Console.WriteLine($"Saved rows: {SavedRows}, Ticket ID: {Message.MessageId}");
        Ticket.AddMessage(UserId,Message);
        return LocalRedirect($"/ticket/{TicketId}");
    }
}