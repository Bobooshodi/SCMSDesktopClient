using SCMSClient.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace SCMSClient.Services.Implementation
{
    public abstract class AbstractService<Model> : IAbstractService<Model>
    {
        #region Inheritable Members

        protected List<Model> allObjects;

        protected string getUrl;
        protected string getAllUrl;
        protected string updateUrl;
        protected string createUrl;
        protected string deleteUrl;

        #endregion


        #region Private Members

        protected IHTTPService httpService;

        #endregion


        #region Default Constructor

        protected AbstractService(IHTTPService _httpService)
        {
            httpService = _httpService;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// combines the <see cref="deleteUrl"/> and the <paramref name="parameter"/>
        /// into a single url and sends an HTTPDelete request to the url on the server
        /// </summary>
        /// <param name="parameter">
        /// the Url pointing to the Delete Controller on the server
        /// </param>
        /// <returns>
        /// returns an object of the Type <see cref="Model"/> specified in the
        /// Inheriting class
        /// </returns>
        public virtual Model Delete(string parameter)
        {
            try
            {
                if (string.IsNullOrEmpty(deleteUrl))
                    throw new InvalidOperationException("Please, provide the Endpoint for this request");

                if (string.IsNullOrEmpty(parameter))
                    throw new InvalidOperationException("Url Parameter not supplied");

                var url = $"{deleteUrl}/{parameter}";

                return httpService.Delete<Model>(url);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// combines the <see cref="getUrl"/> and the <paramref name="parameter"/>
        /// into a single url and sends an HTTPGet request to the url on the server
        /// </summary>
        /// <param name="parameter">
        /// the ID of the object to fetch from the server
        /// </param>
        /// <returns>
        /// returns an object of the Type <see cref="Model"/> specified in the
        /// Inheriting class
        /// </returns>
        public virtual Model Get(string parameter)
        {
            try
            {
                if (string.IsNullOrEmpty(getUrl))
                    throw new InvalidOperationException("Please, provide the Endpoint for this request");

                if (string.IsNullOrEmpty(parameter))
                    throw new InvalidOperationException("Url Parameter not supplied");

                var url = $"{getUrl}/{parameter}";
                return httpService.Get<Model>(url);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Sends an HTTPGet request to the <see cref="getAllUrl"/>
        /// specified in the Inheriting Child Class
        /// </summary>
        /// <returns>
        /// returns a <see cref="List{T}"/>  of objects of the Type <see cref="Model"/> 
        /// specified in the Inheriting class
        /// </returns>
        public virtual List<Model> GetAll()
        {
            try
            {
                if (string.IsNullOrEmpty(getAllUrl))
                    throw new InvalidOperationException("Please, provide the Endpoint for this request");

                return httpService.GetAll<Model>(getAllUrl, null);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Sends an HTTPGet request to the <see cref="createUrl"/>
        /// specified in the Inheriting Child Class
        /// </summary>
        /// <param name="model">
        /// the Model to be sent to the server
        /// </param>
        /// <returns>
        /// returns an object of the Type <see cref="Model"/> specified in the
        /// Inheriting class
        /// </returns>
        public virtual Model Create(Model model)
        {
            try
            {
                if (string.IsNullOrEmpty(createUrl))
                    throw new InvalidOperationException("Please, provide the Endpoint for this request");

                if (EqualityComparer<Model>.Default.Equals(model, default(Model)))
                    throw new InvalidOperationException("Url Parameter not supplied");

                return httpService.Post(model, createUrl);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Sends an HTTPPut request to the <see cref="updateUrl"/>
        /// specified in the Inheriting Child Class
        /// </summary>
        /// <param name="model">
        /// the Model to be sent to the server
        /// </param>
        /// <returns>
        /// returns an object of the Type <see cref="Model"/> specified in the
        /// Inheriting class
        /// </returns>
        public virtual Model Update(Model model)
        {
            try
            {
                if (string.IsNullOrEmpty(updateUrl))
                    throw new InvalidOperationException("Please, provide the Endpoint for this request");

                if (EqualityComparer<Model>.Default.Equals(model, default(Model)))
                    throw new InvalidOperationException("Url Parameter not supplied");

                return httpService.Put(model, updateUrl);
            }
            catch
            {
                throw;
            }
        }

        public virtual void Dispose()
        {
            getAllUrl = null;
            getAllUrl = null;
            createUrl = null;
            deleteUrl = null;
            updateUrl = null;

            httpService.Dispose();
            httpService = null;
        }

        #endregion
    }
}