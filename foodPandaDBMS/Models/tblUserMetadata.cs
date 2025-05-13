using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace foodPandaDBMS.Models
{
    [MetadataType(typeof(tblUserMetadata))]
    public partial class tblUser
    {
        // No changes inside — metadata is applied separately.
    }

    public class tblUserMetadata
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [Display(Name = "Full Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        [Display(Name = "Email Address")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "NIC is required")]
        [Display(Name = "CNIC Number")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "NIC must be 13 digits")]
        public string UserNIC { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [Display(Name = "Password")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "House No is required")]
        [Display(Name = "House No")]
        public string Address_HouseNo { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string Address_City { get; set; }

        [Display(Name = "Status")]
        public string Address_Status { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [Display(Name = "Postal Code")]
        public string Address_PostalCode { get; set; }
    }
}
