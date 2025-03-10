namespace BackendAPI.Dtos
{
    public class UserForRegister
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "User";
    }
}
