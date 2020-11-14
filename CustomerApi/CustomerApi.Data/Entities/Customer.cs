using System.Collections.Generic;

namespace CustomerApi.Data.Entities
{
    /// <summary>
    /// Class to hold the customer data
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }

        public string MobileNo { get; set; }

        public bool IsActive { get; set; }

        public List<Address> Addresses { get; set; }
    }
}
