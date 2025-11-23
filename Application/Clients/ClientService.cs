using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Model.Dtos;

namespace Application.Clients
{
    public class ClientService : IClientService
    {

        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ClientService> _logger;

        public ClientService(
            IClientRepository clientRepository,
            ILogger<ClientService> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ClientDto>> GetClientsAsync()
        {
            return (await _clientRepository.GetAllAsync()).Select(x => new ClientDto { ClientId = x.Id, Email = x.Email, Name = x.Name });
        }

        public async Task<ClientStatusDto> GetClientStatusAsync(Guid clientId)
        {
            var client = await _clientRepository.GetWithPositionsAsync(clientId)
                         ?? throw new InvalidOperationException("Client not found.");

            var status = new ClientStatusDto
            {
                ClientId = client.Id,
                Name = client.Name,
                Email = client.Email,
                CashBalance = client.CashBalance,
                StockPositions = client.Positions
                    .Select(p => new StockPositionDto
                    {
                        StockId = p.StockId,
                        Shares = p.Quantity,
                        StockName = p.Stock?.Name ?? string.Empty,
                        StockPrice = p.Stock?.CurrentPrice ?? 0,
                    }).ToList()
            };

            _logger.LogInformation("Fetched status for Client {ClientId}: Cash {Balance}, {Positions} stock positions",
                clientId, client.CashBalance, status.StockPositions.Count);

            return status;
        }
    }
}
