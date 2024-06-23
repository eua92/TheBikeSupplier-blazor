using System;
using System.ComponentModel.DataAnnotations;
using TheBikeSupplier.Resources;

namespace TheBikeSupplier.Models.Client.Authorize
{
    public class LoginParameters
    {
        [Required(ErrorMessageResourceName = "PleaseEnterYourEmail", ErrorMessageResourceType = typeof(StringResources))]
        [EmailAddress(ErrorMessageResourceName = "EmailAddressNotValid", ErrorMessageResourceType = typeof(StringResources))]
        public string UserName { get; set; }
        
        [Required(ErrorMessageResourceName = "PleaseEnterYourPassword", ErrorMessageResourceType = typeof(StringResources))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}
