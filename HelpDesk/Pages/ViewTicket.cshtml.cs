using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HelpDesk.Models;
using HelpDesk.Data;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Migrations;
using System.ComponentModel.DataAnnotations;

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
    [BindProperty]
    [StringLength(50)]
    public string NewLabelName {get;set;} = string.Empty;
    public List<Label> Labels {get;set;} = [];

    // Get the text from ViewTicket.cshtml written by the user.
    [BindProperty]
    public string Text {get;set;} = string.Empty;

    // Get the ticket, if it exists.
    public async Task<IActionResult> OnGetAsync(int Id)
    {
        Ticket = await database.Tickets
            .Include(Ticket => Ticket.Labels)
            .Include(Ticket => Ticket.User)
            .Include(Ticket => Ticket.Exchange)
                .ThenInclude(message => message.User)
            .SingleOrDefaultAsync(Ticket => Ticket.TicketId == Id);

        Labels = await database.Labels
            .AsNoTracking()
            .OrderBy(label => label.Name)
            .ToListAsync();
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

    public async Task<IActionResult> OnPostChangeStatusAsync(int TicketId)
    {
        Ticket? Ticket = await database.Tickets.SingleOrDefaultAsync(ticket => ticket.TicketId == TicketId);
        if (Ticket == null)
        {
            return NotFound();
        }
        Ticket.Open = !Ticket.Open;
        await database.SaveChangesAsync();
        return RedirectToPage("/ViewTicket", new { id = Ticket.TicketId });
    }

    public async Task<IActionResult> OnPostAddLabelAsync(int TicketId, string NewLabelName)
    {
        Ticket? ticket = await database.Tickets
            .Include(ticket => ticket.Labels)
            .SingleOrDefaultAsync(ticket => ticket.TicketId == TicketId);
        if (ticket == null)
        {
            return NotFound();
        }
        Label? label = null;
        if (!string.IsNullOrWhiteSpace(NewLabelName))
        {
            label = await database.Labels
                .FirstOrDefaultAsync(existingLabel => existingLabel.Name == NewLabelName);

            if (label == null)
            {
                label = new Label{Name = NewLabelName};
                database.Labels.Add(label);
                ticket.Labels.Add(label);
                await database.SaveChangesAsync();
            } else
            {
                bool alreadyAdded = ticket.Labels.Any(existingLabel => existingLabel.LabelId == label.LabelId || existingLabel.Name == label.Name);
                if (!alreadyAdded)
                {
                    ticket.Labels.Add(label);
                    await database.SaveChangesAsync();
                }
            }
        }
        return RedirectToPage("/ViewTicket", new { id = TicketId });
    }
}