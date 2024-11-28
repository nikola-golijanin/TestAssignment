using Microsoft.EntityFrameworkCore;
using TestAssignmentApi.Data;
using TestAssignmentApi.Models;

namespace TestAssignmentApi.Services.Clients;

public class ClientService : IClientService
{
    private readonly ApplicationDbContext _context;

    public ClientService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateClientAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
    }

    public Task<List<Client>> GetClientsAsync() => _context.Clients.ToListAsync();
}
