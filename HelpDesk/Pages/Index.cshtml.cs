using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HelpDesk.Models;
using Microsoft.EntityFrameworkCore.Storage;
using HelpDesk.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Pages;

public class IndexModel : PageModel
{
    public List<Ticket> Tickets { get; set; } = [];
    private readonly HelpDeskDbContext database;
    public IndexModel(HelpDeskDbContext database)
    {
        this.database = database;
    }
    public async Task OnGetAsync()
    {
        Tickets = await database.Tickets
            .AsNoTracking()
            .OrderByDescending(ticket => ticket.CreationDate)
            .ToListAsync();
    }
}