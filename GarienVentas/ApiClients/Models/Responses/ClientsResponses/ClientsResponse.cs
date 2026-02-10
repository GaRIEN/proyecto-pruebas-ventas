

namespace ApiClients.Models.Responses.ClientsResponses
{
    public class ClientsResponse
    {
        public int? ClientId { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
