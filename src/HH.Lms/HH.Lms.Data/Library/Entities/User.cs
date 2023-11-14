using System.ComponentModel.DataAnnotations;
using HH.Lms.Common.Entity;
using Microsoft.AspNetCore.Identity;

namespace HH.Lms.Data.Library.Entities;

public class User : TrackableEntity
{
    public int Id { get; set; }

    [PersonalData] public string FirstName { get; set; }

    [PersonalData] public string LastName { get; set; }

    public string Role { get; set; }

    [Display(Name = "Name")] public string FullName => $"{FirstName} {LastName}";

    // Linking to Book (One to many)
    public ICollection<Book>? Books { get; set; }
}
