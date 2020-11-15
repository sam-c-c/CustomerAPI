using CustomerApi.Data.Entities;
using System.Collections.Generic;

namespace CustomerApi.Data.Providers
{
    public interface ICustomerDataProvider
    {
        /// <summary>
        /// Check to see if the customer already exists.
        /// </summary>
        /// <param name="customer">The customer details to check</param>
        /// <returns>True if already exists, else false</returns>
        bool DoesCustomerAlreadyExist(Customer customer);

        /// <summary>
        /// Adds a new customer record
        /// </summary>
        /// <param name="customer">The customer details to add</param>
        /// <returns>The customerId of the new record</returns>
        int AddCustomer(Customer customer);

        /// <summary>
        /// Deletes a customer record
        /// </summary>
        /// <param name="customerId">The customerId to delete</param>
        void DeleteCustomer(int customerId);

        /// <summary>
        /// Updates the isActive flag of the customer record
        /// </summary>
        /// <param name="customerId">The customerId of the record to update</param>
        /// <param name="isActive">The value that the isActive flag should be set to</param>
        void UpdateCustomerIsActiveFlag(int customerId, bool isActive);

        /// <summary>
        /// Gets a list of all customers
        /// </summary>
        /// <returns>A list of all customers</returns>
        List<Customer> GetAllCustomers();

        /// <summary>
        /// Get a single customer record
        /// </summary>
        /// <param name="customerId">The customerId of the record to return</param>
        /// <returns>The customer record</returns>
        Customer GetCustomer(int customerId);

        /// <summary>
        /// Adds a new Address record
        /// </summary>
        /// <param name="address">The address details to add</param>
        /// <returns>The addressId of the new record</returns>
        int AddAddress(Address address);

        /// <summary>
        /// Deletes an address for a specific customer
        /// </summary>
        /// <param name="customerId">The customerId related to the address</param>
        /// <param name="addressId">The addressId of the record to delete</param>
        void DeleteAddress(int customerId, int addressId);
    }
}
