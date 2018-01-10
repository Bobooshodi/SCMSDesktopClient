using SCMSClient.Services.Implementation;
using SCMSClient.Services.Interfaces;
using Unity;

namespace SCMSClient.Utilities
{
    public static class UnityConfig
    {
        private static IUnityContainer _container;

        public static IUnityContainer Initialize()
        {
            var container = new UnityContainer();

            RegisterTypes(container);
            _container = container;

            return _container;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IHTTPService, HTTPRequestService>();
        }

        public static T ResolveObject<T>(string name = null)
        {
            return _container.Resolve<T>(name);
        }
    }
}
