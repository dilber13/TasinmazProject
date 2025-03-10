using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace BackendAPI.Entities
{
    public class User
    {
        [Key]
        public int userId { get; set; }
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string role { get; set; } 
    }
}
