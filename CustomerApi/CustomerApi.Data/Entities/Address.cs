namespace CustomerApi.Data.Entities
{
    /// <summary>
    /// Class to hold address data
    /// </summary>
    public class Address
    {
        public int Id { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string Town { get; set; }

        public string County { get; set; }

        public string Postcode { get; set; }

        public string Country { get; set; }

        public bool IsMainAddress { get; set; }
    }
}
