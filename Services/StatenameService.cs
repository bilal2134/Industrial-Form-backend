using AutoMapper;
using Enwage_API.DTOs;
using Enwage_API.Models;
using Enwage_API.UnitOfWork.Interfaces;

namespace Enwage_API.Services
{
    public class StatenameService : IStatenameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StatenameService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StatenameDto>> GetAllStatenamesAsync()
        {
            var statenames = await _unitOfWork.Statenames.GetAllAsync();
            return _mapper.Map<IEnumerable<StatenameDto>>(statenames);
        }

        public async Task<StatenameDto> GetStatenameByIdAsync(Guid id)
        {
            var statename = await _unitOfWork.Statenames.GetByIdAsync(id);
            return _mapper.Map<StatenameDto>(statename);
        }

        public async Task<StatenameDto> CreateStatenameAsync(StatenameDto statenameDto)
        {
            var statename = _mapper.Map<Statename>(statenameDto);
            statename.Id = Guid.NewGuid(); // Ensure a new unique ID is generated
            await _unitOfWork.Statenames.AddAsync(statename);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<StatenameDto>(statename);
        }

        public async Task<StatenameDto> UpdateStatenameAsync(Guid id, StatenameDto statenameDto)
        {
            var existingStatename = await _unitOfWork.Statenames.GetByIdAsync(id);
            if (existingStatename == null)
            {
                throw new ArgumentException("Statename not found");
            }

            // Do not update the Id
            existingStatename.Name = statenameDto.Name;
            existingStatename.EmployeeStatenames = _mapper.Map<ICollection<EmployeeStatename>>(statenameDto.EmployeeStatenames);

            _unitOfWork.Statenames.Update(existingStatename);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<StatenameDto>(existingStatename);
        }

        public async Task DeleteStatenameAsync(Guid id)
        {
            var statename = await _unitOfWork.Statenames.GetByIdAsync(id);
            if (statename == null)
            {
                throw new ArgumentException("Statename not found");
            }

            _unitOfWork.Statenames.Remove(statename);
            await _unitOfWork.CompleteAsync();
        }
    }
}
