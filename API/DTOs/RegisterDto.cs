using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        [Required][StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
    }
}