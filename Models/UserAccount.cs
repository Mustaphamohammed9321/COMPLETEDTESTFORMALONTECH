using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MALON_GLOBAL_WEBAPP.Models
{
    public class UserAccount
    {
        [Key]
        [Required]
        [Column(TypeName = "int")]
        public int UserId { get; set; }

        [DataType(DataType.Text)]
        [Column(TypeName = "nvarchar(30)")]
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "This field is required")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Column(TypeName = "nvarchar(30)")]
        [Required(ErrorMessage = "This field is required")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [Column(TypeName = "nvarchar(50)")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage ="This field is required")]
        [DataType(DataType.Password)]
        [Column(TypeName = "nvarchar(30)"), MaxLength(12)]
        [Display(Name = "Password")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{6,}$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Try again!")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }

        [NotMapped]
        public string AuthenticationErrorMessage { get; set; }
    }
}
