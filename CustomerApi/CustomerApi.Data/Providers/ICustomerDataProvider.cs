using CustomerApi.Data.Entities;
using System.Collections.Generic;

namespace CustomerApi.Data.Providers
{
    public interface ICustomerDataProvider
    {
        bool DoesCustomerAlreadyExist(Customer customer);

        int AddCustomer(Customer customer);

        void DeleteCustomer(int customerId);

        void UpdateCustomerIsActiveFlag(int customerId, bool isActive);

        List<Customer> GetAllCustomers();

        Customer GetCustomer(int customerId);

        int AddAddress(Address address);

        void DeleteAddress(int customerId, int addressId);
    }
}
