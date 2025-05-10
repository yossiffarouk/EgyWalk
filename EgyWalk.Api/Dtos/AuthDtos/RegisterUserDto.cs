using System.ComponentModel.DataAnnotations;

namespace EgyWalk.Api.Dtos.AuthDtos
{
    public class RegisterUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string[] Rols { get; set; }
    }
}
