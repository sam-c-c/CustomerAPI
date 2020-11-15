using CustomerApi.Data.Entities;
using CustomerApi.Models;

namespace CustomerApi.Mappers
{
    public class AddressModelToAddressEntityMapper
    {
        public static Address Map(int customerId, AddressModel addressModel)
        {
            return new Address()
            {
                CustomerId = customerId,
                AddressLine1 = addressModel.AddressLine1,
                AddressLine2 = addressModel.AddressLine2,
                Town = addressModel.Town,
                County = addressModel.County,
                Country = addressModel.Country,
                Postcode = addressModel.Postcode,
                IsMainAddress = addressModel.IsMainAddress
            };
        }
    }
}
