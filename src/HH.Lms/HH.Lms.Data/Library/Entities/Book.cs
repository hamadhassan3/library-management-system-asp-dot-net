using HH.Lms.Common.Entity;

namespace HH.Lms.Data.Library.Entities;

public class Book : TrackableEntity
{
    public int Id { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }

    // Linking Book to User
    public int? UserId { get; set; }
    public User? User { get; set; }
}
