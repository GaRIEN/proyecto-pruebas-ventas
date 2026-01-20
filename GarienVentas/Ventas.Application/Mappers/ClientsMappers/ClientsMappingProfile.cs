using AutoMapper;
using Ventas.Application.Responses.ClientsResponses;
using Ventas.Core.Entities.Models;

namespace Ventas.Application.Mappers.ClientsMappers
{
    public class ClientsMappingProfile : Profile
    {
        public ClientsMappingProfile()
        {
            CreateMap<Client, ClientsResponse>().ReverseMap();
        }

    }
}
