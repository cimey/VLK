using Domain.Entities;

namespace Domain.Repositories
{
    public interface IClientRepository : IRepositoryBase<Client, Guid>
    {
        Task<Client?> GetWithPositionsAsync(Guid id);
    }
}
