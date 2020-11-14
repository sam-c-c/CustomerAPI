using CustomerApi.Data.Entities;

namespace CustomerApi.Data.Providers
{
    public interface ICustomerDataProvider
    {
        bool DoesCustomerAlreadyExist(Customer customer);

        int AddCustomer(Customer customer);
    }
}
