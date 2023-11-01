using HH.Lms.Data.Common;

namespace HH.Lms.Data.Library.Entities
{
    public class Book : TrackableEntity, IBaseEntity
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

        // Linking Book to User
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
