﻿using System.ComponentModel.DataAnnotations;

namespace finebe.webapi.Src.Models.ResetPassword;

public class ResetPasswordRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
}
