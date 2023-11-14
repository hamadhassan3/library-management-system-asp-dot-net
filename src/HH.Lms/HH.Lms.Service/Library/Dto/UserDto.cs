using HH.Lms.Service.Base;

namespace HH.Lms.Service.Library.Dto
{
    public class UserDto : IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        public ICollection<BookDto>? Books { get; set; }

    }
}
