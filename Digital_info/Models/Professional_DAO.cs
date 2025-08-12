using MySql.Data.MySqlClient;

namespace Digital_info.Models
{
    public class Professional_DAO
    {

        public List<Professional> getAll()
        {
            List<Professional> col = new List<Professional>();
            string query = "select * from professionnal";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Prepare();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                col.Add(new Professional(int.Parse(reader["id_professional"].ToString()), int.Parse(reader["siret"].ToString()), reader["firstname"].ToString(), reader["lastname"].ToString(), reader["project_qualification"].ToString(), reader["project_datail"].ToString(), reader["availability"].ToString(), double.Parse(reader["price"].ToString()), reader["success"].ToString(), reader["email"].ToString(), reader["password"].ToString(), reader["avatar"].ToString()));
            }
            reader.Close();
            return col;
        }

		public Professional login(int siret,string email, string password)
		{
			Professional obj;
			string query = "select * from professionnal WHERE email=@email and siret =@siret";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Parameters.AddWithValue("@email", email);
			command.Parameters.AddWithValue("@siret", siret);
			command.Prepare();

			MySqlDataReader reader = command.ExecuteReader();
			if (reader.Read())
			{
				if (BCrypt.Net.BCrypt.Verify(password, reader["password"].ToString()))
				{
					obj = new Professional(int.Parse(reader["id_professional"].ToString()), int.Parse(reader["siret"].ToString()), reader["firstname"].ToString(), reader["lastname"].ToString(), reader["project_qualification"].ToString(), reader["project_datail"].ToString(), reader["availability"].ToString(), double.Parse(reader["price"].ToString()), reader["success"].ToString(), reader["email"].ToString(), reader["password"].ToString(), reader["avatar"].ToString()) ;
				}
				else
				{
					obj = null;
				}
			}
			else
			{
				obj = null;
			}
			reader.Close();
			return obj;
		}
		/*public List<ProfessionalViewModel> getAllByIdVM(int id)
		{
			List<ProfessionalViewModel> colProfessionalVM = new List<ProfessionalViewModel>();
			string query = "SELECT * FROM services,professionnal LEFT JOIN experience ON professionnal.id_professional = experience.professional_id WHERE professionnal.id_professional=services.professional_id AND id_professional=@id_professional";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Parameters.AddWithValue("@id_professional", id);
			command.Prepare();
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{

				ProfessionalViewModel ProfessionalVM = new ProfessionalViewModel(
					new Professional(int.Parse(reader["id_professional"].ToString()), int.Parse(reader["siret"].ToString()), reader["firstname"].ToString(), reader["lastname"].ToString(), reader["project_qualification"].ToString(), reader["project_datail"].ToString(), reader["availability"].ToString(), double.Parse(reader["price"].ToString()), reader["success"].ToString(), reader["email"].ToString(), reader["password"].ToString(), reader["avatar"].ToString()),
					new Services(int.Parse(reader["id_services"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["service"].ToString(), reader["service_details"].ToString(), reader["brochure"].ToString(), reader["images"].ToString()));
				//new Experience(int.Parse(reader["id_experience"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["projet_carried"].ToString(), double.Parse(reader["early_success"].ToString()), double.Parse(reader["satisfaction_rate"].ToString()))) ;
				//INSERT INTO `experience` (`id_experience`, `professional_id`, `projet_carried`, `early_success`, `satisfaction_rate`) VALUES (NULL, '3', 'Design project manager', '512.5', '8.5')
				colProfessionalVM.Add(ProfessionalVM);
			}
			reader.Close();
			return colProfessionalVM;

		}*/
		public List<ProfessionalViewModel> getAllIVM()
		{
			//SELECT professionnal.*, services.*, experience.* FROM services,professionnal LEFT JOIN experience ON professionnal.id_professional = experience.professional_id WHERE professionnal.id_professional=services.professional_id GROUP BY professionnal.id_professional;
			//SELECT COUNT(id) as count FROM ad WHERE id_sub_category in (SELECT id FROM sub_category WHERE id_category in(SELECT id FROM category WHERE id =@id))
			//SELECT category.id, category.title, category.picture, COUNT(*)FROM category,  sub_category, ad WHERE  category.id = sub_category.id_category AND sub_category.id = ad.id_sub_category GROUP BY category.id
			List<ProfessionalViewModel> colProfessionalVM = new List<ProfessionalViewModel>();
			string query = "SELECT * FROM services,professionnal LEFT JOIN experience ON professionnal.id_professional = experience.professional_id WHERE professionnal.id_professional=services.professional_id";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Prepare();
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{

				ProfessionalViewModel ProfessionalVM = new ProfessionalViewModel(
					new Professional(int.Parse(reader["id_professional"].ToString()), int.Parse(reader["siret"].ToString()), reader["firstname"].ToString(), reader["lastname"].ToString(), reader["project_qualification"].ToString(), reader["project_datail"].ToString(), reader["availability"].ToString(), double.Parse(reader["price"].ToString()), reader["success"].ToString(), reader["email"].ToString(), reader["password"].ToString(), reader["avatar"].ToString()),
					new Services(int.Parse(reader["id_services"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["service"].ToString(), reader["service_details"].ToString(), reader["brochure"].ToString(), reader["images"].ToString()));
					//new Experience(int.Parse(reader["id_experience"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["projet_carried"].ToString(), double.Parse(reader["early_success"].ToString()), double.Parse(reader["satisfaction_rate"].ToString()))) ;
				//INSERT INTO `experience` (`id_experience`, `professional_id`, `projet_carried`, `early_success`, `satisfaction_rate`) VALUES (NULL, '3', 'Design project manager', '512.5', '8.5')
				colProfessionalVM.Add(ProfessionalVM);
			}
			reader.Close();
			return colProfessionalVM;

		}
		public List<ProfessionalViewModel> getAllIdServicesVM(int id)
		{
			//SELECT COUNT(id) as count FROM ad WHERE id_sub_category in (SELECT id FROM sub_category WHERE id_category in(SELECT id FROM category WHERE id =@id))
			//SELECT category.id, category.title, category.picture, COUNT(*)FROM category,  sub_category, ad WHERE  category.id = sub_category.id_category AND sub_category.id = ad.id_sub_category GROUP BY category.id
			List<ProfessionalViewModel> colProfessionalVM = new List<ProfessionalViewModel>();
			string query = "SELECT * FROM professionnal, services WHERE professionnal.id_professional=services.professional_id  GROUP BY  services.id_services=@id";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Parameters.AddWithValue("@id", id);

			command.Prepare();
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{

                ProfessionalViewModel ProfessionalVM = new ProfessionalViewModel(
                    new Professional(int.Parse(reader["id_professional"].ToString()), int.Parse(reader["siret"].ToString()), reader["firstname"].ToString(), reader["lastname"].ToString(), reader["project_qualification"].ToString(), reader["project_datail"].ToString(), reader["availability"].ToString(), double.Parse(reader["price"].ToString()), reader["success"].ToString(), reader["email"].ToString(), reader["password"].ToString()),
                    new Services(int.Parse(reader["id_services"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["service"].ToString(), reader["service_details"].ToString(), reader["brochure"].ToString(), reader["images"].ToString()));

                   // new Experience(int.Parse(reader["id_experience"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["projet_carried"].ToString(), reader["early_success"].ToString(), reader["satifaction"].ToString()));

				colProfessionalVM.Add(ProfessionalVM);
			}
			reader.Close();
			return colProfessionalVM;

		}
		public List<ProfessionalViewModel> getAllIdExperienceVM(int id)
		{
			//SELECT COUNT(id) as count FROM ad WHERE id_sub_category in (SELECT id FROM sub_category WHERE id_category in(SELECT id FROM category WHERE id =@id))
			//SELECT category.id, category.title, category.picture, COUNT(*)FROM category,  sub_category, ad WHERE  category.id = sub_category.id_category AND sub_category.id = ad.id_sub_category GROUP BY category.id
			List<ProfessionalViewModel> colProfessionalVM = new List<ProfessionalViewModel>();
			string query = "SELECT * FROM professionnal,experience WHERE   experience.professional_id=professionnal.id_professional GROUP BY  experience.id_experience=@id";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Parameters.AddWithValue("@id", id);

			command.Prepare();
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{

				ProfessionalViewModel ProfessionalVM = new ProfessionalViewModel(
					new Professional(int.Parse(reader["id_professional"].ToString()), int.Parse(reader["siret"].ToString()), reader["firstname"].ToString(), reader["lastname"].ToString(), reader["project_qualification"].ToString(), reader["project_datail"].ToString(), reader["availability"].ToString(), double.Parse(reader["price"].ToString()), reader["success"].ToString(), reader["email"].ToString(), reader["password"].ToString()),
					//new Services(int.Parse(reader["id_services"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["service"].ToString(), reader["service_details"].ToString(), reader["brochure"].ToString(), reader["images"].ToString()),

					new Experience(int.Parse(reader["id_experience"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["projet_carried"].ToString(), double.Parse(reader["early_success"].ToString()), double.Parse(reader["satifaction"].ToString())));

				colProfessionalVM.Add(ProfessionalVM);
			}
			reader.Close();
			return colProfessionalVM;

		}
		public Professional getById(int id)
        {
           // Professional obj = new Professional();
            string query = "select * from professionnal WHERE id_professional=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);

            command.Parameters.AddWithValue("@id", id);
            command.Prepare();

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            Professional obj = new Professional(int.Parse(reader["id_professional"].ToString()), int.Parse(reader["siret"].ToString()), reader["firstname"].ToString(), reader["lastname"].ToString(), reader["project_qualification"].ToString(), reader["project_datail"].ToString(), reader["availability"].ToString(), double.Parse(reader["price"].ToString()), reader["success"].ToString(), reader["email"].ToString(), reader["password"].ToString(), reader["avatar"].ToString());
            reader.Close();
            return obj;

        }
		public List<Professional> getByIdVM(int id)
		{
			List<Professional> col = new List<Professional>();
			string query = "select * from professionnal WHERE id_professional=@id";
			MySqlCommand command = new MySqlCommand(query, Database.connection);

			command.Parameters.AddWithValue("@id", id);
			command.Prepare();

			MySqlDataReader reader = command.ExecuteReader();
			reader.Read();

			col.Add(new Professional(int.Parse(reader["id_professional"].ToString()), int.Parse(reader["siret"].ToString()), reader["firstname"].ToString(), reader["lastname"].ToString(), reader["project_qualification"].ToString(), reader["project_datail"].ToString(), reader["availability"].ToString(), double.Parse(reader["price"].ToString()), reader["success"].ToString(), reader["email"].ToString(), reader["password"].ToString(), reader["avatar"].ToString()));
			reader.Close();
			return col;

		}
		public void savePassword(Professional obj)
		{

			string query = "UPDATE professionnal SET  password=@password WHERE id_professional=@id_professional";
			MySqlCommand command = new MySqlCommand(query, Database.connection);

			command.Parameters.AddWithValue("@password", obj.Password);
			command.Parameters.AddWithValue("@id_professional", obj.Id_professional);

			command.Prepare();
			command.ExecuteNonQuery();
			Console.WriteLine("UPDATE OK");

		}
		public List<ProfessionalViewModel>  getServiceByType(string type)
		{
			List<ProfessionalViewModel> colProfessionalVM = new List<ProfessionalViewModel>();

			//SELECT * FROM professionnal, services WHERE professionnal.id_professional=services.professional_id AND (service LIKE "%Design project manager%" OR project_qualification LIKE "%Design project manager%"); 
			//SELECT * FROM professionnal, services WHERE professionnal.id_professional=services.professional_id GROUP BY services.service="Design project manager" ORDER BY `professionnal`.`project_qualification` AS
			//SELECT COUNT(id) as count FROM ad WHERE id_sub_category in (SELECT id FROM sub_category WHERE id_category in(SELECT id FROM category WHERE id =@id))
			//SELECT category.id, category.title, category.picture, COUNT(*)FROM category,  sub_category, ad WHERE  category.id = sub_category.id_category AND sub_category.id = ad.id_sub_category GROUP BY category.id
			string query = "SELECT * FROM professionnal, services WHERE professionnal.id_professional=services.professional_id ORDER BY (service LIKE @type OR project_qualification LIKE @type)";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Parameters.AddWithValue("@type", "%" + type + "%");

			command.Prepare();
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{

				ProfessionalViewModel ProfessionalVM = new ProfessionalViewModel(
					new Professional(int.Parse(reader["id_professional"].ToString()), int.Parse(reader["siret"].ToString()), reader["firstname"].ToString(), reader["lastname"].ToString(), reader["project_qualification"].ToString(), reader["project_datail"].ToString(), reader["availability"].ToString(), double.Parse(reader["price"].ToString()), reader["success"].ToString(), reader["email"].ToString(), reader["password"].ToString()),
					new Services(int.Parse(reader["id_services"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["service"].ToString(), reader["service_details"].ToString(), reader["brochure"].ToString(), reader["images"].ToString()));

				// new Experience(int.Parse(reader["id_experience"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["projet_carried"].ToString(), reader["early_success"].ToString(), reader["satifaction"].ToString()));
				colProfessionalVM.Add(ProfessionalVM);


			}
			reader.Close();
			return colProfessionalVM;

		}
		public void save(Professional obj)
        {
            if (obj.Id_professional == 0)
            {
                string query = "INSERT INTO Professionnal (siret,firstname,lastname, project_datail, availability,price,success,email,password) VALUES(@siret,@firstname,@lastname, @project_datail, @availability,@price,@success,@email,@password)";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@siret", obj.Siret);
				command.Parameters.AddWithValue("@firstname", obj.Firstname);
				command.Parameters.AddWithValue("@lastname", obj.Lastname);
				command.Parameters.AddWithValue("@project_datail", obj.Project_datail);
                command.Parameters.AddWithValue("@availability", obj.Availability);
                command.Parameters.AddWithValue("@price", obj.Price);
                command.Parameters.AddWithValue("@success", obj.Success);
                command.Parameters.AddWithValue("@email", obj.Email);
                command.Parameters.AddWithValue("@password", obj.Password);


                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("INSERT OK");
            }
			else if (obj.Password != null)
			{
				string query = "UPDATE Professionnal SET siret=@siret,firstname=@firstname,lastname=@lastname, project_datail=@project_datail, availability=@availability, price=@price,success=@success,email=@email,password=@password WHERE id_professional=@id_professional";
				MySqlCommand command = new MySqlCommand(query, Database.connection);
				command.Parameters.AddWithValue("@siret", obj.Siret);
				command.Parameters.AddWithValue("@firstname", obj.Firstname);
				command.Parameters.AddWithValue("@lastname", obj.Lastname);
				command.Parameters.AddWithValue("@project_datail", obj.Project_datail);
				command.Parameters.AddWithValue("@availability", obj.Availability);
				command.Parameters.AddWithValue("@price", obj.Price);
				command.Parameters.AddWithValue("@success", obj.Success);
				command.Parameters.AddWithValue("@email", obj.Email);
				command.Parameters.AddWithValue("@password", obj.Password);
				command.Parameters.AddWithValue("@id_professional", obj.Id_professional);
				command.Prepare();
				command.ExecuteNonQuery();
				Console.WriteLine("UPDATE OK");
			}
            {
                string query = "UPDATE Professionnal SET siret=@siret,firstname=@firstname,lastname=@lastname, project_datail=@project_datail, availability=@availability, price=@price,success=@success,email=@email,avatar=@avatar WHERE id_professional=@id_professional";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@siret", obj.Siret);
				command.Parameters.AddWithValue("@firstname", obj.Firstname);
				command.Parameters.AddWithValue("@lastname", obj.Lastname);
				command.Parameters.AddWithValue("@project_datail", obj.Project_datail);
                command.Parameters.AddWithValue("@availability", obj.Availability);
                command.Parameters.AddWithValue("@price", obj.Price);
                command.Parameters.AddWithValue("@success", obj.Success);
                command.Parameters.AddWithValue("@email", obj.Email);
				command.Parameters.AddWithValue("@avatar", obj.Avatar);
				command.Parameters.AddWithValue("@id_professional", obj.Id_professional);
                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("UPDATE OK");
            }
        }

        public void delete(int id)
        {
            string query = "DELETE FROM Professional WHERE id_professional=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Parameters.AddWithValue("@id", id);
            command.Prepare();
            command.ExecuteNonQuery();
            Console.WriteLine("DELETE OK");

        }
    }
}
