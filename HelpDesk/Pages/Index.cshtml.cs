using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelpDesk.Pages;

public class IndexModel : PageModel
{
    public List<Ticket> Tickets { get; set; } = ListData.Tickets;

    public void OnGet()
    {
    }
}

