using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var connectionString =
    builder.Configuration.GetConnectionString("HelpdeskDatabase")
    ?? throw new InvalidOperationException(
        "Database connection string is missing.");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

ListData.Tickets = new List<Ticket>
{
    new Ticket(0,"Ticket unavailable","Error getting the ticket"),
    new Ticket(1,"Can't log in","Password reset not working."),
    new Ticket(2,"Feature request","Dark mode please."),
    new Ticket(3,"Billing question","Why is it so expensive?"),
};


app.Run();