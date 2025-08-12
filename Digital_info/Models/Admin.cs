namespace Digital_info.Models
{
    public class Admin
    {
        public int Id_admin { get; set; } 
        public string Email { get; set; }    
        public string Password { get; set; } 
        public Admin() { }

        public Admin(int id_admin, string email, string password)
        {
            Id_admin = id_admin;
            Email = email;
            Password = password;
        }

        public Admin(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
