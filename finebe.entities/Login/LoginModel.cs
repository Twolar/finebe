using System.ComponentModel.DataAnnotations;

namespace finebe.entities.Login;

public class LoginModel
{
    [Required]
    public string Email { get; set; }
    
    [Required] 
    public string Password { get; set; }
}
