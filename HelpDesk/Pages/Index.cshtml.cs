using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HelpDesk.Models;
using Microsoft.EntityFrameworkCore.Storage;
using HelpDesk.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HelpDesk.Pages;

public class IndexModel : PageModel
{
    public List<Ticket> Tickets { get; set; } = [];
    public string SearchInput {get;set;} = string.Empty;
    private readonly HelpDeskDbContext database;
    public IndexModel(HelpDeskDbContext database)
    {
        this.database = database;
    }
    public async Task OnGetAsync()
    {
        Tickets = await database.Tickets
            .AsNoTracking()
            .Include(ticket =>ticket.User)
            .OrderByDescending(ticket => ticket.CreationDate)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostSearchAsync(string SearchInput)
    {
        string[] and_search = SearchInput.Split("AND");
        foreach(string search in and_search)
        {
            string[] andor_search = search.Split("OR");
            foreach(string search_ in andor_search)
            {
                GroupCollection label_matches = new Regex(@"label *= *(\w+)").Match(search_.ToLower()).Groups;
                GroupCollection title_matches = new Regex(@"title *= *(\w+)").Match(search_.ToLower()).Groups;
                GroupCollection text_matches = new Regex(@"text *= *(\w+)").Match(search_.ToLower()).Groups;
            }
        }
        Tickets = await database.Tickets
            .AsNoTracking()
            .Include(ticket => ticket.User)
            .Include(ticket => ticket.Labels)
            .Include(ticket => ticket.UserList)
            .Include(ticket => ticket.Exchange)
            .OrderByDescending(Ticket => Ticket.CreationDate)
            .ToListAsync();
        return LocalRedirect("/Index");
    }
}