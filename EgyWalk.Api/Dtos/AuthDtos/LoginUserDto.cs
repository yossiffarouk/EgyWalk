using System.ComponentModel.DataAnnotations;

namespace EgyWalk.Api.Dtos.AuthDtos
{
    public class LoginUserDto
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
