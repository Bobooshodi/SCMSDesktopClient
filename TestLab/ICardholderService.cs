using SCMSClient.Models;

namespace SCMSClient.Services.Interfaces
{
    public interface ICardholderService : IAbstractService<Cardholder>
    {
        Employee GetEmployee(string cardholderId);

        Tenant GetTenant(string cardholderId);
    }
}