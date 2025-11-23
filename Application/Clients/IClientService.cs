using Model.Dtos;

namespace Application.Clients
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetClientsAsync();

        Task<ClientStatusDto> GetClientStatusAsync(Guid clientId);
    }
}
