namespace GameStore.Application.Exceptions;

public class CloudinaryException : Exception
{
    public CloudinaryException()
    {
    }

    public CloudinaryException(string message) : base(message)
    {
    }

}