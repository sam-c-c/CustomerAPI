using CustomerApi.Data.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
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

        public int AddAddress(Address address)
        {
            var insertAddressStoredProcedure = "InsertAddress";
            var updateAddressIsMainAddressFlagToFalseForCustomer = "UpdateAddressIsMainAddressFlagToFalseForCustomer";

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        if (address.IsMainAddress)
                        {
                            conn.Execute(updateAddressIsMainAddressFlagToFalseForCustomer,
                                new { customerId = address.CustomerId }, transaction, commandType: CommandType.StoredProcedure);
                        }

                        var addressId = conn.ExecuteScalar<int>(insertAddressStoredProcedure, new
                        {
                            customerId = address.CustomerId,
                            addressLine1 = address.AddressLine1,
                            addressLine2 = address.AddressLine2,
                            town = address.Town,
                            county = address.County,
                            postcode = address.Postcode,
                            country = address.Country,
                            isMainAddress = address.IsMainAddress
                        }, transaction, commandType: CommandType.StoredProcedure);
                        transaction.Commit();
                        return addressId;
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public int AddCustomer(Customer customer)
        {
            var insertCustomerStoredProcedure = "InsertCustomer";
            var insertAddressStoredProcedure = "InsertAddress";

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        var customerId = conn.ExecuteScalar<int>(insertCustomerStoredProcedure, new
                        {
                            title = customer.Title,
                            forename = customer.Forename,
                            surname = customer.Surname,
                            emailAddress = customer.EmailAddress,
                            mobileNo = customer.MobileNo
                        }, transaction, commandType: CommandType.StoredProcedure);
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
                            }, transaction, commandType: CommandType.StoredProcedure);
                        }
                        transaction.Commit();
                        return customerId;
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void DeleteAddress(int customerId, int addressId)
        {
            var deleteAddressStoredProcedure = "DeleteAddress";

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Execute(deleteAddressStoredProcedure, new
                {
                    customerId,
                    addressId
                }, commandType: CommandType.StoredProcedure);
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
                }, commandType: CommandType.StoredProcedure);
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
                }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result > 0;
            }
        }

        public List<Customer> GetAllCustomers()
        {
            var allCustomersStoredProcedure = "GetAllCustomers";
            var getCustomerAddressesStoredProcedure = "GetAllAddressesForCustomer";

            using (var conn = new SqlConnection(connectionString))
            {
                var customers = conn.Query<Customer>(allCustomersStoredProcedure, commandType: CommandType.StoredProcedure).ToList();
                if (customers != null && customers.Any())
                {
                    foreach (var customer in customers)
                    {
                        customer.Addresses = conn.Query<Address>(getCustomerAddressesStoredProcedure, 
                            new { customerId = customer.Id }, commandType: CommandType.StoredProcedure).ToList();
                    }
                }
                return customers;
            }
        }

        public Customer GetCustomer(int customerId)
        {
            var getCustomerByIdStoredProcedure = "GetCustomerById";
            var getCustomerAddressesStoredProcedure = "GetAllAddressesForCustomer";

            using (var conn = new SqlConnection(connectionString))
            {
                var customer = conn.Query<Customer>(getCustomerByIdStoredProcedure,
                    new { customerId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                if (customer != null && customer != default)
                {
                    customer.Addresses = conn.Query<Address>(getCustomerAddressesStoredProcedure,
                            new { customerId = customer.Id }, commandType: CommandType.StoredProcedure).ToList();
                }
                return customer;
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
                }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
