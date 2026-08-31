using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelpDesk.Pages;

public class CreateTicketModel : PageModel
{
    [BindProperty]
    public string Title {get;set;} = string.Empty;
    [BindProperty]
    public string Message {get;set;} = string.Empty;
    [BindProperty]
    public string UserId {get;set;} = string.Empty;
    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        return Page();
    }
}