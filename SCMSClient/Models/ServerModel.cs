namespace SCMSClient.Models
{
    public class Server : BaseModel
    {
        private string ipAddress, port, fullUrl;
        private ConnectionProtocol protocol;

        public string FullUrl
        {
            get
            {
                return fullUrl;
            }
            set
            {
                Set(() => FullUrl, ref fullUrl, value);

                RaisePropertyChanged(() => FullUrl);
            }
        }

        public string IpAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                Set(() => IpAddress, ref ipAddress, value);

                RaisePropertyChanged(() => IpAddress);

                FullUrl = BuildCamerUrl();
                RaisePropertyChanged(() => FullUrl);
            }
        }

        public string Port
        {
            get
            {
                return port;
            }
            set
            {
                Set(() => Port, ref port, value);

                RaisePropertyChanged(() => Port);

                FullUrl = BuildCamerUrl();
                RaisePropertyChanged(() => FullUrl);
            }
        }

        public ConnectionProtocol Protocol
        {
            get
            {
                return protocol;
            }
            set
            {
                Set(() => Protocol, ref protocol, value);

                RaisePropertyChanged(() => Protocol);

                FullUrl = BuildCamerUrl();
                RaisePropertyChanged(() => FullUrl);
            }
        }

        public string BuildCamerUrl()
        {
            string cp;

            if (Protocol == ConnectionProtocol.HTTP)
                cp = "http://";
            else
                cp = "https://";

            return string.Format("{0}{1}:{2}", cp, IpAddress, Port);
        }
    }

    public enum TransportProtocol
    {
        TCP,
        UDP
    }

    public enum ConnectionProtocol
    {
        HTTP,
        HTTPS
    }
}
