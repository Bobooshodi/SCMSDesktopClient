using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace SCMSClient.Services.Interfaces
{
    public interface IHTTPService
    {
        T Put<T>(T model, string url);

        Tresult Put<Tmodel, Tresult>(Tmodel model, string url);

        T Post<T>(T model, string url);

        T PostForm<T>(NameValueCollection formData, string url);

        T Get<T>(string url);

        T Get<T>(string url, object parameter);

        List<T> GetAll<T>(string url);

        List<T> GetAll<T>(string url, object parameter);

        T Delete<T>(string url);

        T Delete<T>(string url, object parameter);

        T RefreshAccessToken<T>(string token, string grantType, string clientId);

        Exception ThrowBaseException(Exception ex);
    }
}
