using Enwage_API.DTOs;

namespace Enwage_API.Services
{
    public interface IStatenameService
    {
        Task<IEnumerable<StatenameDto>> GetAllStatenamesAsync();
        Task<StatenameDto> GetStatenameByIdAsync(Guid id);
        Task<StatenameDto> CreateStatenameAsync(StatenameDto statenameDto);
        Task<StatenameDto> UpdateStatenameAsync(Guid id, StatenameDto statenameDto);
        Task DeleteStatenameAsync(Guid id);
    }
}