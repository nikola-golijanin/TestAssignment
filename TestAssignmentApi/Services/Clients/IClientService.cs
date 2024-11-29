using TestAssignmentApi.Models;
using TestAssignmentApi.Utils;

namespace TestAssignmentApi.Services.Clients;

public interface IClientService
{
    Task CreateClientAsync(Client client);
    Task<Result<Client>> GetClientByIdAsync(int id);
    Task<List<Client>> GetClientsAsync();
}
