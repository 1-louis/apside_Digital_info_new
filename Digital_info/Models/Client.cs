namespace Digital_info.Models
{
    public class Client
    {
        public int Id_client { get; set; }
        public string Firstname { get; set; }    
        public string Lastname { get; set; } 
        public string Address { get; set; }  
        public string City { get; set; }  
        public string Phone { get; set; }  
        public int Age { get; set; }  
        public string Email { get; set; }
        public string Password { get; set; }   
        public  Client() { }

		public Client(int id_client, string firstname, string lastname, string address, 
			string city, string phone, int age, string email)
		{
			Id_client = id_client;
			Firstname = firstname;
			Lastname = lastname;
			Address = address;
			City = city;
			Phone = phone;
			Age = age;
			Email = email;
		
		}
		public Client(int id_client, string firstname, string lastname, string address,
			string city, string phone, int age, string email, string password)
		{
			Id_client = id_client;
			Firstname = firstname;
			Lastname = lastname;
			Address = address;
			City = city;
			Phone = phone;
			Age = age;
			Email = email;
			Password = password;

		}
		public Client(string firstname, string lastname, string address, string city,
			string phone, int age, string email, string password)
		{
			Firstname = firstname;
			Lastname = lastname;
			Address = address;
			City = city;
			Phone = phone;
			Age = age;
			Email = email;
            Password = password;
        }
        public Client(int id_client, string password)
        {
			Id_client = id_client;
			Password = password;
			
		}
	}

}
