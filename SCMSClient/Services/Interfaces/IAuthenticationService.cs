using SCMSClient.Models;

namespace SCMSClient.Services.Interfaces
{
    public interface IAuthenticationService
    {
        User RefreshUserLogin(string refreshToken);

        User Login(string username, string password);
    }
}