using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HelpDesk.Data;
using HelpDesk.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Pages;

public class CreateTicketModel : PageModel
{
    private readonly HelpDeskDbContext database;
    public CreateTicketModel(HelpDeskDbContext database)
    {
        this.database = database;
    }
    [BindProperty]
    public string Title {get;set;} = string.Empty;
    [BindProperty]
    public string Text {get;set;} = string.Empty;
    [BindProperty]
    public int UserId {get;set;}
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        bool userExists = await database.Users.AnyAsync(user => user.UserId == UserId);
        if (!userExists)
        {
            ModelState.AddModelError(nameof(UserId),"The selected user does not exist.");
            return Page();
        }
        var Ticket = new Ticket(UserId, Title, Text);
        database.Tickets.Add(Ticket);
        int SavedRows = await database.SaveChangesAsync();
        Console.WriteLine($"Saved rows: {SavedRows}, Ticket ID: {Ticket.TicketId}");

        return RedirectToPage("/ViewTicket",new {id = Ticket.TicketId});
    }
}