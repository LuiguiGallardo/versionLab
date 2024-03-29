using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserDto 
    {   
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}