using Microsoft.AspNetCore.Mvc;
using TestAssignmentApi.Dtos;
using TestAssignmentApi.Models;
using TestAssignmentApi.Services.Clients;

namespace TestAssignmentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;

        private readonly IClientService _clientService;

        public ClientsController(
            ILogger<ClientsController> logger,
            IClientService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clientService.GetClientsAsync();
            return Ok(clients);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewClient([FromBody] CreateNewClientDto newClient)
        {
            var client = new Client
            {
                Name = newClient.ClientName,
                ApiKey = Guid.NewGuid()
            };

            await _clientService.CreateClientAsync(client);
            _logger.LogInformation("New client created successfully");
            return NoContent();
        }
    }
}
