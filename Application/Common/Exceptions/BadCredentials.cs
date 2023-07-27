namespace Application.Common.Exceptions;

public class BadCredentials : Exception
{
    public BadCredentials(string message) : base(message)
    {
        
    }
}