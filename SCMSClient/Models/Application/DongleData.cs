using CardEncoderLib;
using System.Collections.Generic;

namespace SCMSClient.Models
{
    public class DongleData
    {
        public List<CardKey> CardKeys { get; set; }
        public List<PCInfo> PCToBoind { get; set; }
    }

    public class PCInfo
    {
        public string Uid { get; set; }
    }
}