using System.ComponentModel.DataAnnotations.Schema;

namespace Tourism.Domain.Models;

public class User
{
    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }
    public string Role { get; set; }
}