using HelpDesk.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Data;

public class HelpDeskDbContext : DbContext
{
    public HelpDeskDbContext(
        DbContextOptions<HelpDeskDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Ticket> Tickets => Set<Ticket>();

    public DbSet<Message> TicketMessages => Set<Message>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();

        modelBuilder.Entity<Ticket>()
            .HasOne(ticket => ticket.User)
            .WithMany(user => user.Tickets)
            .HasForeignKey(ticket => ticket.User)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(message => message.Ticket)
            .WithMany(ticket => ticket.Exchange)
            .HasForeignKey(message => message.Ticket)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Message>()
            .HasOne(message => message.User)
            .WithMany()
            .HasForeignKey(message => message.User)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ticket>()
            .HasIndex(ticket => ticket.UserId);

        modelBuilder.Entity<Ticket>()
            .HasIndex(ticket => ticket.Open);

        modelBuilder.Entity<Message>()
            .HasIndex(message => message.TicketId);
    }
}