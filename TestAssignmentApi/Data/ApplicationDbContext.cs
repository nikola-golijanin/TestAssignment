using Microsoft.EntityFrameworkCore;
using TestAssignmentApi.Models;

namespace TestAssignmentApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; }
}
