namespace BLL.Services.Interfaces;
public interface IAccountService
{
    /// <summary>
    /// Login in this application
    /// </summary>
    /// <param name="username">username</param>
    /// <param name="password">password</param>
    /// <returns>JWT Token</returns>
    public string LogIn(string username, string password); 


}
