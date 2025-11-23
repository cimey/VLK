using Application.Clients;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos;
using Model.Responses;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {

        private readonly IClientService _clientService;
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientService.GetClientsAsync();
            return Ok(ApiResponse<IEnumerable<ClientDto>>.Ok(clients, "Clients fetched."));
        }

        [HttpGet("status/{clientId}")]
        public async Task<IActionResult> Status(Guid clientId)
        {
            var status = await _clientService.GetClientStatusAsync(clientId);
            return Ok(ApiResponse<ClientStatusDto>.Ok(status, "Client status fetched."));
        }
    }
}
