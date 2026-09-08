using System.ComponentModel.DataAnnotations;
using HelpDesk.Data;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Models;

public class Label
{
    public int LabelId {get;set;}
    [Required]
    public string Name {get;set;} = string.Empty;
    public List<Ticket> Tickets {get;set;} = [];
}