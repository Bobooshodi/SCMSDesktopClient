using SCMSClient.Models;
using SCMSClient.Services.Interfaces;

namespace SCMSClient.Services.Implementation
{
    public class UserService : AbstractService<User>, IUserService
    {
        #region Default Constructor

        public UserService(IHTTPService httpService) : base(_httpService: httpService)
        {

        }

        #endregion


        #region Public Methods



        #endregion
    }
}
