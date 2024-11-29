using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TestAssignmentApi.Dtos;
using TestAssignmentApi.Models;
using TestAssignmentApi.Services.Clients;

namespace TestAssignmentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : BaseController
    {
        private readonly ILogger<ClientsController> _logger;

        private readonly IClientService _clientService;

        public ClientsController(
            ILogger<ClientsController> logger,
            IClientService clientService) : base(logger)
        {
            _logger = logger;
            _clientService = clientService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all clients", Description = "Retrieves all clients.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Clients retrieved successfully", typeof(List<Client>))]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clientService.GetClientsAsync();
            return Ok(clients);
        }

        [HttpGet("{id:int}", Name = nameof(GetClientAsync))]
        [SwaggerOperation(Summary = "Get a client by ID", Description = "Retrieves a client by their unique ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Client retrieved successfully", typeof(Client))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Client not found")]
        public async Task<IActionResult> GetClientAsync(int id)
        {
            var result = await _clientService.GetClientByIdAsync(id);

            if (result.IsFailure)
                return ResolveErrors(error: result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new client", Description = "Creates a new client with the provided details.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Client created successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid client data")]
        public async Task<IActionResult> CreateNewClient([FromBody] CreateNewClientDto newClient)
        {

            if (!ModelState.IsValid)
                return ResolveErrors(ModelState);

            var client = new Client
            {
                Name = newClient.ClientName,
                ApiKey = Guid.NewGuid()
            };

            await _clientService.CreateClientAsync(client);
            _logger.LogInformation("New client created successfully");
            return CreatedAtRoute(nameof(GetClientAsync), new { id = client.Id }, client);
        }
    }
}
