﻿namespace finebe.webapi.Src.Models.Settings;

public class SendGridSettings
{
    public string ApiKey { get; set; }

    public string FromEmail { get; set; }

    public string FromName { get; set; }
}