using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Models
{
    /// <summary>
    /// A model class used to hold the customer data
    /// </summary>
    public class CustomerModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "The maximum length of Title is 20 characters")]
        public string Title { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The maximum length of Forename is 50 characters")]
        public string Forename { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The maximum length of Surname is 50 characters")]
        public string Surname { get; set; }

        [Required]
        [MaxLength(75, ErrorMessage = "The maximum length of EmailAddress is 75 characters")]
        [EmailAddress(ErrorMessage = "EmailAddress is invalid")]
        public string EmailAddress { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The maximum length of MobileNo is 20 characters")]
        public string MobileNo { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "There must be atleast 1 address")]
        public List<AddressModel> Addresses { get; set; }
    }
}
