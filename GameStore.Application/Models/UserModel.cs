﻿namespace GameStore.Application.Models;

public class UserModel
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public ImageModel Image { get; set; }
}