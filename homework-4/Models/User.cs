using System.ComponentModel.DataAnnotations;

namespace homework_4.Models;

public class User(string login, string email, string password)
{
    public int Id { get; set; } = -1;
    
    [Required(ErrorMessage = "Login is required")]
    public string? Login { get; set; } = login;
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Incorrect email")]
    public string? Email { get; set; } = email;
    
    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string? Password { get; set; } = password;
}