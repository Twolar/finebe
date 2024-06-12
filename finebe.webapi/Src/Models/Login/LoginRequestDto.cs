using System.ComponentModel.DataAnnotations;

namespace finebe.webapi.Src.Models.Login;

public class LoginRequestDto
{
    [Required]
    public string Email { get; set; }
    
    [Required] 
    public string Password { get; set; }
}
