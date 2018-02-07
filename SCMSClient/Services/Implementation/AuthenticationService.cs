using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;
using System;
using System.Web;

namespace SCMSClient.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private IHTTPService httpService;

        public AuthenticationService(IHTTPService _httpService)
        {
            httpService = _httpService;
        }

        /// <summary>
        /// Sends the User's details to the server to authenticate the User
        /// </summary>
        /// <param name="username"> Username of the User </param>
        /// <param name="password">Password of the User </param>
        /// <returns>
        /// A <see cref="User"/> object if the Authentication passes or
        /// Throws an <see cref="System.Exception"/> if the Authentication
        /// fails
        /// </returns>
        public User Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new InvalidOperationException("Username or Password not present");

            try
            {
                const string grantType = "password";
                const string clientId = "desktop";

                var content = HttpUtility.ParseQueryString(string.Empty);
                content.Add("username", username);
                content.Add("password", password);
                content.Add("grant_type", grantType);
                content.Add("client_id", clientId);

                return httpService.PostForm<User>(content, ApiEndpoints.login);
            }
            catch
            {
                throw;
            }
        }

        public User RefreshUserLogin(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("No token was Passed");

            try
            {
                const string grantType = "password";
                const string clientId = "desktop";

                var content = HttpUtility.ParseQueryString(string.Empty);
                content.Add("refresh_token", token);
                content.Add("grant_type", grantType);
                content.Add("client_id", clientId);

                return httpService.PostForm<User>(content, ApiEndpoints.login);
            }
            catch
            {
                throw;
            }
        }

    }
}
