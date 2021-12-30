using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.ThirdParty.Data.Model
{
    [Table("Users")]
    public class Users
    {
        public long UserId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(20, ErrorMessage = "First name is too long.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20, ErrorMessage = "Last name is too long.")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:mm/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Email Address")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(11, ErrorMessage = "Invalid Phone Number Length.")]
        public string PhoneNumber { get; set; }

    }
}
