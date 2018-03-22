using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using SCMSClient.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Windows;

namespace SCMSClient.Services.Implementation
{
    public class HTTPRequestServiceDev : IHTTPService
    {
        #region Private Members

        private HttpClient client;
        private ISettingsService _sSettings;
        private User appUser = (User)Application.Current.Properties["activeUser"];

        #endregion Private Members

        #region Default Constructor

        public HTTPRequestServiceDev(ISettingsService sSettings)
        {
            _sSettings = sSettings;
            //userService = _userService;

            //TODO: Uncomment Later

            /**
            try
            {
                if (appSettings == null)
                    appSettings = _sSettings.LoadSettings();

                if (string.IsNullOrEmpty(appSettings.RemoteServer.FullUrl))
                    throw new Exception("Remote Server not Set Yet For HTTP Client to use");
            }
            catch (Exception e)
            {
                ErrorLogger.LogError(e.ToString(), ErrorType.APPLICATION_ERROR);

                throw;
            }
    */

            InitialiseClient();
        }

        #endregion Default Constructor

        #region Private Methods

        /// <summary>
        /// Initialise the <see cref="HttpClient"/> to be used for communication in this class
        /// </summary>
        private void InitialiseClient()
        {
            try
            {
                client = new HttpClient();

                if (appUser != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", appUser.Access_token);
                }

                client.Timeout = new TimeSpan(0, 10, 0);
                // client.BaseAddress = new Uri(appSettings.RemoteServer.FullUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri("http://localhost:3000/");
            }
            catch
            {
            }
        }

        /// <summary>
        /// Processes the <see cref="HttpResponseMessage"/> and returns the
        /// appropriate Type <typeparamref name="T"/> of object
        /// </summary>
        /// <typeparam name="T">
        /// The Type <see cref="Type"/> of object expected
        /// </typeparam>
        /// <param name="response">
        /// The response <see cref="HttpResponseMessage"/> gotten from the server
        /// </param>
        /// <returns>
        /// The Type <see cref="Type"/> of object passes in the call
        /// </returns>
        /// <exception cref="Exception">
        /// If the server returns an error, this Exception is raised with the appropriate Message
        /// </exception>
        private T ReturnResult<T>(HttpResponseMessage response)
        {
            try
            {
                if (!response.IsSuccessStatusCode)
                {
                    ErrorLogger.LogError(response.ToString(), ErrorType.SERVER_ERROR);
                    var obj = response.Content.ReadAsAsync<object>().Result;
                    ThrowExceptionMessage(obj);
                }

                return response.Content.ReadAsAsync<T>().Result;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("No MediaTypeFormatter is available"))
                {
                    throw new Exception("Please Try again an error occoured on the server");
                }

                throw ThrowBaseException(ex);
            }
        }

        /// <summary>
        /// Tries to format the Error from the Server to a <see cref="BadRequestError"/>
        /// to be displayed to the User
        /// </summary>
        /// <param name="obj">
        /// The error Object returned from the server
        /// </param>
        private void ThrowExceptionMessage(object obj)
        {
            var data = obj.ToString();
            var processed = false;

            BadRequestError badError = null;

            try
            {
                badError = Newtonsoft.Json.JsonConvert.DeserializeObject<BadRequestError>(data);
                processed = true;
            }
            catch { }

            if (processed)
                throw new Exception(badError.GetErrorMessage());
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Sends an HTTP PUT request to the url <paramref name="url"/> on the server
        /// containing the Model <paramref name="model"/> passed in the call and waits for the
        /// result of the operation
        /// </summary>
        /// <typeparam name="T">
        /// The return Type <see cref="Type"/> of the object expected
        /// </typeparam>
        /// <param name="model">
        /// The Model object to send to the Server
        /// </param>
        /// <param name="url">
        /// A Url On the Server to send the request to
        /// </param>
        /// <returns>
        /// An object of the Type <see cref="Type"/> Tresult <typeparamref name="T"/> Specified in the call
        /// </returns>
        public T Put<T>(T model, string url)
        {
            try
            {
                HttpResponseMessage response = client.PutAsJsonAsync(url, model).Result;

                return ReturnResult<T>(response);
            }
            catch (Exception ex)
            {
                throw ThrowBaseException(ex.GetBaseException());
            }
        }

        /// <summary>
        /// Sends an HTTP PUT request to the url <paramref name="url"/> on the server
        /// containing the Model <paramref name="model"/> passed in the call and waits for the
        /// result of the operation
        /// </summary>
        /// <typeparam name="Tmodel">
        /// The Type <see cref="Type"/> of the model object to send to the Server
        /// </typeparam>
        /// <typeparam name="Tresult">
        /// The Type <see cref="Type"/> of object expected from the server
        /// </typeparam>
        /// <param name="model">
        /// The Actual object to send to the server
        /// </param>
        /// <param name="url">
        /// A Url On the Server to send the request to
        /// </param>
        /// <returns>
        /// An object of the Type <see cref="Type"/> Tresult <typeparamref name="Tresult"/> Specified in the call
        /// </returns>
        public Tresult Put<Tmodel, Tresult>(Tmodel model, string url)
        {
            try
            {
                HttpResponseMessage response = client.PutAsJsonAsync(url, model).Result;

                return ReturnResult<Tresult>(response);
            }
            catch (Exception ex)
            {
                throw ThrowBaseException(ex.GetBaseException());
            }
        }

        /// <summary>
        /// Sends an HTTP POST request to the url <paramref name="url"/> on the server
        /// containing the Model <paramref name="model"/> passed in the call and waits for the
        /// result of the operation
        /// </summary>
        /// <typeparam name="T">
        /// The return Type <see cref="Type"/> of the object expected
        /// </typeparam>
        /// <param name="model">
        /// The Model object to send to the Server
        /// </param>
        /// <param name="url">
        /// A Url On the Server to send the request to
        /// </param>
        /// <returns>
        /// An object of the Type <see cref="Type"/> Tresult <typeparamref name="T"/> Specified in the call
        /// </returns>
        public T Post<T>(T model, string url)
        {
            try
            {
                HttpResponseMessage response = client.PostAsJsonAsync(url, model).Result;

                return ReturnResult<T>(response);
            }
            catch (Exception ex)
            {
                throw ThrowBaseException(ex.GetBaseException());
            }
        }

        /// <summary>
        /// Sends an HTTP POST request to the url <paramref name="url"/> on the server
        /// containing the Model <paramref name="model"/> passed in the call and waits for the
        /// result of the operation
        /// </summary>
        /// <typeparam name="Tmodel">
        /// The Type <see cref="Type"/> of the model object to send to the Server
        /// </typeparam>
        /// <typeparam name="Tresult">
        /// The Type <see cref="Type"/> of object expected from the server
        /// </typeparam>
        /// <param name="model">
        /// The Actual object to send to the server
        /// </param>
        /// <param name="url">
        /// A Url On the Server to send the request to
        /// </param>
        /// <returns>
        /// An object of the Type <see cref="Type"/> Tresult <typeparamref name="Tresult"/> Specified in the call
        /// </returns>
        public Tresult Post<Tmodel, Tresult>(Tmodel model, string url)
        {
            try
            {
                HttpResponseMessage response = client.PostAsJsonAsync(url, model).Result;

                return ReturnResult<Tresult>(response);
            }
            catch (Exception ex)
            {
                throw ThrowBaseException(ex.GetBaseException());
            }
        }

        /// <summary>
        /// Posts Form Data to the Server and waits for rhe result of the operation
        /// </summary>
        /// <typeparam name="T">
        /// The return Type <see cref="Type"/> of the object expected
        /// </typeparam>
        /// <param name="formData">
        /// The Form Data to send to the server
        /// </param>
        /// <param name="url">
        /// A Url On the Server to send the request to
        /// </param>
        /// <returns>
        /// An object of the Type <see cref="Type"/> Tresult <typeparamref name="T"/> Specified in the call
        /// </returns>
        public T PostForm<T>(NameValueCollection formData, string url)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                foreach (var k in formData.AllKeys)
                {
                    dict.Add(k, formData[k]);
                }

                var content = new FormUrlEncodedContent(dict);

                HttpResponseMessage response = client.PostAsync(url, content).Result;

                return ReturnResult<T>(response);
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("No MediaTypeFormatter"))
                    throw new Exception("Server not found");

                throw ThrowBaseException(ex.GetBaseException());
            }
        }

        /// <summary>
        /// Concatenates the Url <paramref name="url"/> and the parameter <paramref name="parameter"/>
        /// into a single Url and Sends an HTTP GET request to the resulting url and waits for the
        /// result of the operation
        /// </summary>
        /// <typeparam name="T">
        /// The return Type <see cref="Type"/> of the object expected
        /// </typeparam>
        /// <param name="parameter">
        /// the parameter to pass to the URL
        /// </param>
        /// <param name="url">
        /// A Url On the Server to send the request to
        /// </param>
        /// <returns>
        /// An object of the Type <see cref="Type"/> Tresult <typeparamref name="T"/> Specified in the call
        /// </returns>
        public T Get<T>(string url, object parameter)
        {
            url += parameter.ToString();

            try
            {
                HttpResponseMessage response = client.GetAsync(url).Result;

                return ReturnResult<T>(response);
            }
            catch (Exception ex)
            {
                throw ThrowBaseException(ex.GetBaseException());
            }
        }

        /// <summary>
        /// Sends an HTTP GET request to the url <paramref name="url"/> on the server
        /// and waits for the result of the operation
        /// </summary>
        /// <typeparam name="T">
        /// The return Type <see cref="Type"/> of the object expected from the server
        /// </typeparam>
        /// <param name="url">
        /// A Url On the Server to send the request to
        /// </param>
        /// <returns>
        /// An object of the Type <see cref="Type"/> Tresult <typeparamref name="T"/> Specified in the call
        /// </returns>
        public T Get<T>(string url)
        {
            try
            {
                HttpResponseMessage response = client.GetAsync(url).Result;

                return ReturnResult<T>(response);
            }
            catch (Exception ex)
            {
                throw ThrowBaseException(ex.GetBaseException());
            }
        }

        /// <summary>
        /// Sends an HTTP Get request to the url <paramref name="url"/> on the server
        /// and waits for the result of the operation
        /// </summary>
        /// <typeparam name="T">
        /// The return Type <see cref="Type"/> of the object expected from the server
        /// </typeparam>
        /// <param name="url">
        /// A Url On the Server to send the request to
        /// </param>
        /// <returns>
        /// A list of the object of the Type <see cref="Type"/> T <typeparamref name="T"/>
        /// Specified in the call
        /// </returns>
        public List<T> GetAll<T>(string url)
        {
            url += "all";

            try
            {
                HttpResponseMessage response = client.GetAsync(url).Result;

                return ReturnResult<List<T>>(response);
            }
            catch (Exception ex)
            {
                throw ThrowBaseException(ex.GetBaseException());
            }
        }

        /// <summary>
        /// Sends an HTTP Get request to the url <paramref name="url"/> on the server
        /// and waits for the result of the operation
        /// </summary>
        /// <typeparam name="T">
        /// The return Type <see cref="Type"/> of the object expected from the server
        /// </typeparam>
        /// <param name="url">
        /// A Url On the Server to send the request to
        /// </param>
        /// <param name="parameter">
        /// a parameter to pass to the Url
        /// </param>
        /// <returns>
        /// A list of the object of the Type <see cref="Type"/> Tresult <typeparamref name="T"/>
        /// Specified in the call
        /// </returns>
        public List<T> GetAll<T>(string url, object parameter = null)
        {
            if (parameter != null)
                url += parameter.ToString();

            try
            {
                HttpResponseMessage response = client.GetAsync(url).Result;

                return ReturnResult<List<T>>(response);
            }
            catch (Exception ex)
            {
                throw ThrowBaseException(ex.GetBaseException());
            }
        }

        /// <summary>
        /// Sends an HTTP DELETE request to the url <paramref name="url"/> on the server
        /// and waits for the result of the operation
        /// </summary>
        /// <typeparam name="T">
        /// The return Type <see cref="Type"/> of the object expected
        /// </typeparam>
        /// <param name="url">
        /// A Url On the Server to send the request to
        /// </param>
        /// <returns>
        /// the deleted object the Type <see cref="Type"/> T <typeparamref name="T"/>
        /// Specified in the call
        /// </returns>
        public T Delete<T>(string url)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync(url).Result;

                return ReturnResult<T>(response);
            }
            catch (Exception ex)
            {
                throw ThrowBaseException(ex.GetBaseException());
            }
        }

        /// <summary>
        /// Concatenates the Url <paramref name="url"/> and the parameter <paramref name="parameter"/>
        /// into a single Url and Sends an HTTP DELETE request to the resulting url and waits for the
        /// result of the operation
        /// </summary>
        /// <typeparam name="T">
        /// The return Type <see cref="Type"/> of the object expected
        /// </typeparam>
        /// <param name="parameter">
        /// the parameter to pass to the URL
        /// </param>
        /// <param name="url">
        /// A Url On the Server to send the request to
        /// </param>
        /// <returns>
        /// An object of the Type <see cref="Type"/> Tresult <typeparamref name="T"/> Specified in the call
        /// </returns>
        public T Delete<T>(string url, object parameter)
        {
            url += parameter.ToString();

            try
            {
                HttpResponseMessage response = client.DeleteAsync(url).Result;

                return ReturnResult<T>(response);
            }
            catch (Exception ex)
            {
                throw ThrowBaseException(ex.GetBaseException());
            }
        }

        public bool UserClaimsExpired()
        {
            if (appUser == null)
            {
                appUser = Application.Current.Properties["loggedInUser"] as User;
            }

            try
            {
                if (appUser.Access_token != null)
                {
                    var jwthandler = new JwtSecurityTokenHandler();
                    var jwttoken = jwthandler.ReadToken(appUser.Access_token);
                    var expDate = jwttoken.ValidTo;
                    if (expDate < DateTime.UtcNow.AddMinutes(1))
                    {
                        return true;
                    }

                    return false;
                }

                throw new RefreshTokenException("Can't retrieve User's Access token, \r\n you will be logged out now, please login again to begin a new Session");
            }
            catch
            {
                throw;
            }
        }

        public T RefreshAccessToken<T>(string token, string grantType, string clientId)
        {
            try
            {
                var content = HttpUtility.ParseQueryString(string.Empty);
                content.Add("refresh_token", token);
                content.Add("grant_type", grantType);
                content.Add("client_id", clientId);

                return PostForm<T>(content, ApiEndpoints.login);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the base Exception <see cref="Exception"/> of the <paramref name="ex"/>
        /// passed to it
        /// </summary>
        /// <param name="ex">
        /// The Exception <see cref="Exception"/> to get it's base Exception
        /// </param>
        /// <returns>
        /// An the base Exception <see cref="Exception"/>
        /// </returns>
        public Exception ThrowBaseException(Exception ex)
        {
            if (ex is RefreshTokenException)
            {
                MessageBox.Show(ex.Message);
                _sSettings.LogOutUser();
            }

            throw ex.GetBaseException();
        }

        private void Dispose(bool disposing)
        {
            client?.Dispose();
            client = null;

            if (disposing)
            {
                _sSettings = null;
                appUser = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Destructor

        ~HTTPRequestServiceDev()
        {
            Dispose(false);
        }

        #endregion Destructor
    }
}