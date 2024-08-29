using AutoMapper;
using Enwage_API.DTOs;
using Enwage_API.Models;
using Enwage_API.UnitOfWork.Interfaces;

namespace Enwage_API.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            var clients = await _unitOfWork.Clients.GetAllAsync();
            return _mapper.Map<IEnumerable<ClientDto>>(clients);
        }

        public async Task<ClientDto> GetClientByIdAsync(Guid id)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(id);
            if (client == null)
            {
                throw new ArgumentException("Client not found");
            }
            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto> CreateClientAsync(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            client.Id = Guid.NewGuid(); // Generate a new ID for the client

            await _unitOfWork.Clients.AddAsync(client);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto> UpdateClientAsync(Guid id, ClientDto clientDto)
        {
            var existingClient = await _unitOfWork.Clients.GetByIdAsync(id);
            if (existingClient == null)
            {
                throw new ArgumentException("Client not found");
            }

            _mapper.Map(clientDto, existingClient);
            existingClient.Id = id; // Ensure the ID remains unchanged

            _unitOfWork.Clients.Update(existingClient);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ClientDto>(existingClient);
        }

        public async Task DeleteClientAsync(Guid id)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(id);
            if (client == null)
            {
                throw new ArgumentException("Client not found");
            }

            _unitOfWork.Clients.Remove(client);
            await _unitOfWork.CompleteAsync();
        }
    }
}