using SCMSClient.Models;

namespace SCMSClient.Services.Interfaces
{
    public interface IDinkeyDongleService
    {
        bool IsDonglePresent();

        bool CheckProtection();

        bool WriteDongleData(DongleData data);

        DongleData GetDongleData();
    }
}