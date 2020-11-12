using AutoMapper;
using Sysplan.Application.ViewModels;
using Sysplan.Domain.Commands;

namespace Sysplan.Application.Mappings
{
    public class ViewModelToCommandMappingProfile : Profile
    {
        public ViewModelToCommandMappingProfile()
        {
            CreateMap<ClienteViewModel, AddClienteCommand>();
            CreateMap<ClienteViewModel, UpdateClienteCommand>();
            CreateMap<ClienteViewModel, RemoveClienteCommand>();
        }
    }
}