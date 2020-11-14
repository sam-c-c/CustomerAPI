using CustomerApi.Data.Entities;
using CustomerApi.Models;
using System.Collections.Generic;

namespace CustomerApi.Mappers
{
    /// <summary>
    /// Mapper class to map a customer model class to a customer class
    /// </summary>
    public class CustomerModelToCustomerEntityMapper
    {
        public static Customer Map(CustomerModel customerModel)
        {
            return new Customer()
            {
                Title = customerModel.Title,
                Forename = customerModel.Forename,
                Surname = customerModel.Surname,
                EmailAddress = customerModel.EmailAddress,
                MobileNo = customerModel.MobileNo,
                IsActive = customerModel.IsActive,
                Addresses = MapAddresses(customerModel)
            };
        }

        private static List<Address> MapAddresses(CustomerModel customerModel)
        {
            var addresses = new List<Address>();
            foreach (var address in customerModel.Addresses)
            {
                addresses.Add(new Address()
                {
                    AddressLine1 = address.AddressLine1,
                    AddressLine2 = address.AddressLine2,
                    Town = address.Town,
                    County = address.County,
                    Country = address.Country,
                    Postcode = address.Postcode,
                    IsMainAddress = address.IsMainAddress
                });
            }

            return addresses;
        }
    }
}
