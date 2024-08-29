using AutoMapper;
using Enwage_API.DTOs;
using Enwage_API.Models;

namespace Enwage_API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
           

            CreateMap<Employee, EmployeeDto>()
        .ForMember(dest => dest.States, opt => opt.MapFrom(src =>
            src.EmployeeStatenames != null ? src.EmployeeStatenames.Select(es => es.StatenameId).ToList() : new List<Guid>()))
        .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src =>
            src.Client != null ? src.Client.Name : string.Empty));



            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.EmployeeStatenames, opt => opt.Ignore());

            CreateMap<Fileattachment, FileAttachmentDto>();
            CreateMap<FileAttachmentDto, Fileattachment>();

            CreateMap<Statename, StatenameDto>();
            CreateMap<StatenameDto, Statename>();

            CreateMap<Statename, StatenameDto>().ReverseMap();
            CreateMap<EmployeeStatename, EmployeeStatenameDto>().ReverseMap();

            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>();

            CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
        }

    }
}
