using HH.Lms.Data.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HH.Lms.Data.Library.Entities
{
    public class User : IdentityUser, IBaseEntity
    {
        [PersonalData] public string FirstName { get; set; }

        [PersonalData] public string LastName { get; set; }

        public string Role { get; set; }

        [Display(Name = "Name")] public string FullName => $"{FirstName} {LastName}";

        // Linking to Book (One to many)
        public ICollection<Book> Books { get; set; }
    }
}
