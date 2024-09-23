using System.Security.Principal;

namespace Domain.Entities;

public class User : Entity
{
    public string Username { get; set; }

    public string FirstName { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public List<string> Roles { get; set; }
}
