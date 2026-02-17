
namespace Ventas.Application.Responses.UsuarioResponses
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Activo { get; set; }

    }
}
