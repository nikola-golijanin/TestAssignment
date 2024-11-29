using Microsoft.EntityFrameworkCore;
using TestAssignmentApi.Data;
using TestAssignmentApi.Models;
using TestAssignmentApi.Models.Errors;
using TestAssignmentApi.Utils;

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

    public async Task<Result<Client>> GetClientByIdAsync(int id)
    {
        var client = await _context.Clients
                                .AsNoTracking()
                                .Where(u => u.Id == id)
                                .FirstOrDefaultAsync();
        return client is null
            ? Result<Client>.Failure(ClientErrors.NotFound)
            : Result<Client>.Success(client);
    }

    public Task<List<Client>> GetClientsAsync() => _context.Clients.ToListAsync();
}
