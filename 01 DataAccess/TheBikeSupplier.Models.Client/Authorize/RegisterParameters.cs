using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Resources;

namespace TheBikeSupplier.Models.Client.Authorize
{
    public class RegisterParameters
    {     
        [Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(StringResources))]
        [EmailAddress(ErrorMessageResourceName = "EmailAddressNotValid", ErrorMessageResourceType = typeof(StringResources))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "FirstNameRequired", ErrorMessageResourceType = typeof(StringResources))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "LastNameRequired", ErrorMessageResourceType = typeof(StringResources))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "PhoneNumberRequired", ErrorMessageResourceType = typeof(StringResources))]
        [Phone(ErrorMessageResourceName = "PhoneNumberNotValid", ErrorMessageResourceType = typeof(StringResources))]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(StringResources))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessageResourceName = "PasswordConfirmationDoesNotMatch", ErrorMessageResourceType = typeof(StringResources))]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceName = "DateOfBirthFieldRequired", ErrorMessageResourceType = typeof(StringResources))]
        public DateTime? DateOfBirth => YearOfBirth.HasValue && MonthOfBirth.HasValue && DayOfBirth.HasValue ? new DateTime(YearOfBirth.Value, MonthOfBirth.Value, DayOfBirth.Value, 0, 0, 0) : null;

        public int? MonthOfBirth { get; set; }
        public int? DayOfBirth { get; set; }
        public int? YearOfBirth { get; set; }
    }
}
