﻿using System.ComponentModel.DataAnnotations;

namespace finebe.webapi.Src.Models.ResetPassword;

public class ValidateResetTokenRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Token { get; set; }
}
