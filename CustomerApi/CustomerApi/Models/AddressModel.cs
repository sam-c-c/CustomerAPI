using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Models
{
    public class AddressModel
    {
        [Required]
        [MaxLength(80, ErrorMessage = "The maximum length of AddressLine1 is 80 characters")]
        public string AddressLine1 { get; set; }

        [MaxLength(80, ErrorMessage = "The maximum length of AddressLine2 is 80 characters")]
        public string AddressLine2 { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The maximum length of Town is 50 characters")]
        public string Town { get; set; }

        [MaxLength(50, ErrorMessage = "The maximum length of County is 50 characters")]
        public string County { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "The maximum length of Postcode is 10 characters")]
        public string Postcode { get; set; }

        [MaxLength(10, ErrorMessage = "The maximum length of Country is 10 characters")]
        public string Country { get; set; }

        public bool IsMainAddress { get; set; }
    }
}
