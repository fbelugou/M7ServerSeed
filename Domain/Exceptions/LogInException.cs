namespace Domain.Exceptions;
public class LogInException : Exception
{
    public LogInException(string username): base($"{DateTime.Now} - Authentification failed for : {username}")
    {
        
    }
}
