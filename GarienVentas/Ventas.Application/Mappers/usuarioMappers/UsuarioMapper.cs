

using AutoMapper;
using Ventas.Application.Mappers.ClientsMappers;

namespace Ventas.Application.Mappers.usuarioMappers
{
    public static class UsuarioMapper
    {
        private static readonly Lazy<IMapper> _mapper = new(static () =>
        {
            var expression = new MapperConfigurationExpression();

            expression.ShouldMapProperty = p =>
                p.GetMethod!.IsPublic || p.GetMethod.IsAssembly;

            expression.AddProfile<UsuarioMappingProfile>();

            var config = new MapperConfiguration(expression);
            return config.CreateMapper();
        });

        public static IMapper Mapper => _mapper.Value;
    }
}
