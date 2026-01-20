using AutoMapper;

namespace Ventas.Application.Mappers.ClientsMappers
{
    public static class ClientsMapper
    {
        private static readonly Lazy<IMapper> _mapper = new(static () =>
        {
            var expression = new MapperConfigurationExpression();

            expression.ShouldMapProperty = p =>
                p.GetMethod!.IsPublic || p.GetMethod.IsAssembly;

            expression.AddProfile<ClientsMappingProfile>();

            var config = new MapperConfiguration(expression);
            return config.CreateMapper();
        });

        public static IMapper Mapper => _mapper.Value;

    }
}
