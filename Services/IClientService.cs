using Enwage_API.DTOs;

namespace Enwage_API.Services
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAllClientsAsync();
        Task<ClientDto> GetClientByIdAsync(Guid id);
        Task<ClientDto> CreateClientAsync(ClientDto clientDto);
        Task<ClientDto> UpdateClientAsync(Guid id, ClientDto clientDto);
        Task DeleteClientAsync(Guid id);
    }
}