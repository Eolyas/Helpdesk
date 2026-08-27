using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelpDesk.Pages;

public class CreateTicketModel : PageModel
{
    [BindProperty]
    public string title {get;set;} = string.Empty;
    [BindProperty]
    public string message {get;set;} = string.Empty;
    [BindProperty]
    public string user_id {get;set;} = string.Empty;
    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        return Page();
    }
}