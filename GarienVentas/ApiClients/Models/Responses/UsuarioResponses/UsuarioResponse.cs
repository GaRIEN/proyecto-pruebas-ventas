
namespace ApiClients.Models.Responses.UsuarioResponses
{
    public record UsuarioResponse
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Activo { get; set; }
    }
}
