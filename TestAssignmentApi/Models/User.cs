namespace TestAssignmentApi.Models;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string PhoneNumber { get; set; }

    public string Culture { get; set; }

    public string Language { get; set; }

    public string PasswordSalt { get; set; }
}
