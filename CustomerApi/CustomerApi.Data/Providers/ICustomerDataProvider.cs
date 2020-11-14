using CustomerApi.Data.Entities;

namespace CustomerApi.Data.Providers
{
    public interface ICustomerDataProvider
    {
        bool DoesCustomerAlreadyExist(Customer customer);

        int AddCustomer(Customer customer);

        void DeleteCustomer(int customerId);

        void UpdateCustomerIsActiveFlag(int customerId, bool isActive);
    }
}
