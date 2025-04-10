using System.ComponentModel.DataAnnotations.Schema;

namespace stepTogether.Models
{
    public class UserRegisterDto
    {
        [Column("username")]
        public string Username { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("passwordHash")]
        public string Password { get; set; }
    }

}
