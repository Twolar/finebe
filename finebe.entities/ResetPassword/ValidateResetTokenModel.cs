using System.ComponentModel.DataAnnotations;

namespace finebe.entities.ResetPassword;

public class ValidateResetTokenModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Token { get; set; }
}
