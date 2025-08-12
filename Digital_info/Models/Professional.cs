namespace Digital_info.Models
{
    public class Professional
    {
        public int Id_professional { get; set; }    
        public int Siret { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Project_qualification { get; set; }

		public string Avatar { get; set; }

		public string Project_datail { get; set; }  
        public string Availability { get; set; }
        public double Price { get; set; }
        public string Success { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
		public Professional() { }

		public Professional(int id_professional, int siret, string firstname, string lastname, string project_qualification, string project_datail, string availability, double price, string success, string email, string password, string avatar)
		{
			Id_professional = id_professional;
			Siret = siret;
			Firstname = firstname;
			Lastname = lastname;
			Project_qualification = project_qualification;
			Project_datail = project_datail;
			Availability = availability;
			Price = price;
			Success = success;
			Email = email;
			Password = password;
			Avatar = avatar;
		}

		public Professional(int id_professional, int siret, string firstname, string lastname, string project_qualification, string project_datail, string availability, double price, string success, string email,string avatar)
		{
			Id_professional = id_professional;
			Siret = siret;
			Firstname = firstname;
			Lastname = lastname;
			Project_qualification = project_qualification;
			Project_datail = project_datail;
			Availability = availability;
			Price = price;
			Success = success;
			Email = email;
			Avatar = avatar;
		}
		

		public Professional(int siret, string firstname, string lastname, string project_qualification, string project_datail, string availability, double price, string success, string email)
		{
			Siret = siret;
			Firstname = firstname;
			Lastname = lastname;
			Project_qualification = project_qualification;
			Project_datail = project_datail;
			Availability = availability;
			Price = price;
			Success = success;
			Email = email;
			
		}

		public Professional(int siret, string firstname, string lastname, string project_qualification, string project_datail, string availability, double price, string success, string email, string password)
		{
			Siret = siret;
			Firstname = firstname;
			Lastname = lastname;
			Project_qualification = project_qualification;
			Project_datail = project_datail;
			Availability = availability;
			Price = price;
			Success = success;
			Email = email;
			Password = password;
		}
		public Professional(int id_professional, string password)
		{
			Id_professional = id_professional;
			Password = password;
		}
	}
}
