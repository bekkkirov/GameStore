﻿namespace GameStore.Application.Exceptions;

public class IdentityException : Exception
{
    public IdentityException()
    {
    }

    public IdentityException(string message) : base(message)
    {
    }
}