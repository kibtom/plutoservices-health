using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PlutoHealthWeb.Models
{
    [BindProperties(SupportsGet = true)]
    public class UserFeedbackForm
    {
         [Display(Name = "Your Name")]
        [Required(ErrorMessage = "{0} is a required.")]
        [StringLength(40, ErrorMessage = "{0} cannot be longer than 40 characters.")]
        public string UserName { get; set; }


        [Display(Name = "Your Postcode")]
        [Required(ErrorMessage = "{0} is a required.")]
        [StringLength(12, ErrorMessage = "{0} cannot be longer than 12 characters.")]
        public string UserPostcode { get; set; }


        [Display(Name = "Your Email Address")]
        [Required(ErrorMessage = "{0} is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(40, ErrorMessage = "{0} cannot be longer than 40 characters.")]
        public string UserEmailAddress { get; set; }

        [Display(Name = "Your Phone Number")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(15, ErrorMessage="{0} cannot exceed 15 characters")]
        public string UserPhoneNumber { get; set; }

        [Display(Name = "Your Message")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(1000, ErrorMessage = "{0} cannot be longer than 1000 characters.")]
        public string UserMessage { get; set; }
    }
}