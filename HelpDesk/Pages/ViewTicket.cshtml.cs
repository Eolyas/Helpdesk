using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HelpDesk.Models;
using HelpDesk.Data;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Pages;

// Handle the backend of the page that shows singular tickets.
// It also handles adding messages to tickets
public class ViewTicketModel : PageModel
{
    private readonly HelpDeskDbContext database;
    public ViewTicketModel(HelpDeskDbContext database)
    {
        this.database = database;
    }
    public Ticket? Ticket { get; set; }

    // Get the text from ViewTicket.cshtml written by the user.
    [BindProperty]
    public string Text {get;set;} = string.Empty;

    // Get the ticket, if it exists.
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

    // Add a message to a ticket and add it to the database.
    public async Task<IActionResult> OnPostSendMessageAsync(int TicketId, int UserId)
    {
        // Get the curret ticket
        Ticket = await database.Tickets.SingleOrDefaultAsync(ticket => ticket.TicketId == TicketId);
        if (Ticket is null)
        {
            return NotFound();
        }
        // WIP Get the the current user. It doesn't handle different users other than the creator of the ticket as the login isn't linked.
        User? User = await database.Users.SingleOrDefaultAsync(user => user.UserId == UserId);
        if (User is null)
        {
            return BadRequest("User not found");
        }
        // Check if the there is a message to add.
        if (string.IsNullOrWhiteSpace(Text))
        {
            return LocalRedirect($"/ticket/{TicketId}");
        }
        // Create a new Message, add it to the database, save it, add its reference to the Ticket for links.
        Message Message = new Message(UserId, TicketId, Text);
        database.TicketMessages.Add(Message);
        await database.SaveChangesAsync();
        Ticket.AddMessage(UserId,Message);

        return LocalRedirect($"/ticket/{TicketId}");
    }
}