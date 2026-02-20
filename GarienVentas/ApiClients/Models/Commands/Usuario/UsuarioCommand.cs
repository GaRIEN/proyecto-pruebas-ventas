
namespace ApiClients.Models.Commands.Usuario
{
    public record UsuarioCommand
    {
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
    }
}
