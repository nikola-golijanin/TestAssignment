using TestAssignmentApi.Models;

namespace TestAssignmentApi.Services.Clients;

public interface IClientService
{
    Task CreateClientAsync(Client client);
    Task<List<Client>> GetClientsAsync();
}
