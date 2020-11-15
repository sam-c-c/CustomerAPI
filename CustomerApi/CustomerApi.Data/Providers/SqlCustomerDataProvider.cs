using CustomerApi.Data.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CustomerApi.Data.Providers
{
    public class SqlCustomerDataProvider : ICustomerDataProvider
    {
        private readonly string connectionString;

        public SqlCustomerDataProvider(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("CustomerApiDb");
        }

        public int AddCustomer(Customer customer)
        {
            var insertCustomerStoredProcedure = "InsertCustomer";
            var insertAddressStoredProcedure = "InsertAddress";

            using (var conn = new SqlConnection(connectionString))
            {
                var customerId = conn.ExecuteScalar<int>(insertCustomerStoredProcedure, new
                {
                    title = customer.Title,
                    forename = customer.Forename,
                    surname = customer.Surname,
                    emailAddress = customer.EmailAddress,
                    mobileNo = customer.MobileNo
                }, commandType: System.Data.CommandType.StoredProcedure);
                foreach (var address in customer.Addresses)
                {
                    conn.Execute(insertAddressStoredProcedure, new
                    {
                        customerId,
                        addressLine1 = address.AddressLine1,
                        addressLine2 = address.AddressLine2,
                        town = address.Town,
                        county = address.County,
                        postcode = address.Postcode,
                        country = address.Country,
                        isMainAddress = address.IsMainAddress
                    }, commandType: System.Data.CommandType.StoredProcedure);
                }
                return customerId;
            }
        }

        public void DeleteCustomer(int customerId)
        {
            var deleteCustomerStoredProcedure = "DeleteCustomer";

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Execute(deleteCustomerStoredProcedure, new
                {
                    customerId,
                }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public bool DoesCustomerAlreadyExist(Customer customer)
        {
            var storedProcedure = "DoesCustomerAlreadyExist";

            using (var conn = new SqlConnection(connectionString))
            {
                var result = conn.Query<int>(storedProcedure, new { 
                    title = customer.Title, 
                    forename = customer.Forename, 
                    surname = customer.Surname, 
                    emailAddress = customer.EmailAddress 
                }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                return result > 0;
            }
        }

        public List<Customer> GetAllCustomers()
        {
            var allCustomersStoredProcedure = "GetAllCustomers";
            var getCustomerAddressesStoredProcedure = "GetAllAddressesForCustomer";

            using (var conn = new SqlConnection(connectionString))
            {
                var customers = conn.Query<Customer>(allCustomersStoredProcedure, commandType: System.Data.CommandType.StoredProcedure).ToList();
                if (customers != null && customers.Any())
                {
                    foreach (var customer in customers)
                    {
                        customer.Addresses = conn.Query<Address>(getCustomerAddressesStoredProcedure, 
                            new { customerId = customer.Id }, commandType: System.Data.CommandType.StoredProcedure).ToList();
                    }
                }
                return customers;
            }
        }

        public void UpdateCustomerIsActiveFlag(int customerId, bool isActive)
        {
            var updateCustomerIsActiveFlagStoredProcedure = "UpdateCustomerIsActiveFlag";

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Execute(updateCustomerIsActiveFlagStoredProcedure, new
                {
                    customerId,
                    isActive
                }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
