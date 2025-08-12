using MySql.Data.MySqlClient;
using Stripe;

namespace Digital_info.Models
{
    public class Experience_DAO
    {
        public List<Experience> getAll()
        {
            List<Experience> col = new List<Experience>();
            string query = "select * from Experience";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Prepare();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                col.Add(new Experience(int.Parse(reader["id_experience"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["projet_carried"].ToString(), double.Parse(reader["early_success"].ToString()), double.Parse(reader["satisfaction_rate"].ToString())));
            }
            reader.Close();
            return col;
        }
		public List<Experience> getAllBestSucces()
		{
			List<Experience> col = new List<Experience>();
			string query = "SELECT * FROM `experience` ORDER BY `experience`.`early_success` DESC LIMIT 3";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Prepare();
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				col.Add(new Experience(int.Parse(reader["id_experience"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["projet_carried"].ToString(), double.Parse(reader["early_success"].ToString()), double.Parse(reader["satisfaction_rate"].ToString())));
			}
			reader.Close();
			return col;
		}
		
		public List<Experience> getAllbyBest()
		{
			List<Experience> col = new List<Experience>();
			string query = "SELECT * FROM `experience` ORDER BY `experience`.`early_success` DESC LIMIT 3";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Prepare();
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				col.Add(new Experience(int.Parse(reader["id_experience"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["projet_carried"].ToString(), double.Parse(reader["early_success"].ToString()), double.Parse(reader["satisfaction_rate"].ToString())));
			}
			reader.Close();
			return col;
		}
		public List<ProfessionalViewModel> getBestByNBR(int id)
        {
           

			List<ProfessionalViewModel> colProfessionalVM = new List<ProfessionalViewModel>();
			string query = "SELECT* FROM professionnal, services,experience WHERE" +
				" professionnal.id_professional=services.professional_id AND " +
				"professionnal.id_professional=experience.professional_id " +
				"ORDER BY `experience`.`early_success` DESC LIMIT @id";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Parameters.AddWithValue("@id", id);

			command.Prepare();
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{

				ProfessionalViewModel ProfessionalVM = new ProfessionalViewModel(
					new Professional(int.Parse(reader["id_professional"].ToString()), int.Parse(reader["siret"].ToString()), reader["firstname"].ToString(), reader["lastname"].ToString(), reader["project_qualification"].ToString(), reader["project_datail"].ToString(), reader["availability"].ToString(), double.Parse(reader["price"].ToString()), reader["success"].ToString(), reader["email"].ToString(), reader["password"].ToString()),
					new Services(int.Parse(reader["id_services"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["service"].ToString(), reader["service_details"].ToString(), reader["brochure"].ToString(), reader["images"].ToString()),
					new Experience(int.Parse(reader["id_experience"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["projet_carried"].ToString(), double.Parse(reader["early_success"].ToString()), double.Parse(reader["satisfaction_rate"].ToString())));
				//INSERT INTO `experience` (`id_experience`, `professional_id`, `projet_carried`, `early_success`, `satisfaction_rate`) VALUES (NULL, '3', 'Design project manager', '512.5', '8.5')
				colProfessionalVM.Add(ProfessionalVM);
			}
			reader.Close();
			return colProfessionalVM;

		}
		public List<ProfessionalViewModel> getBestExpVM()
		{
			//SELECT professionnal.*, services.*, experience.* FROM services,professionnal LEFT JOIN experience ON professionnal.id_professional = experience.professional_id WHERE professionnal.id_professional=services.professional_id GROUP BY professionnal.id_professional;
			//SELECT COUNT(id) as count FROM ad WHERE id_sub_category in (SELECT id FROM sub_category WHERE id_category in(SELECT id FROM category WHERE id =@id))
			//SELECT category.id, category.title, category.picture, COUNT(*)FROM category,  sub_category, ad WHERE  category.id = sub_category.id_category AND sub_category.id = ad.id_sub_category GROUP BY category.id
			List<ProfessionalViewModel> colProfessionalVM = new List<ProfessionalViewModel>();
			string query = "SELECT * FROM professionnal, services,experience WHERE professionnal.id_professional=services.professional_id AND professionnal.id_professional=experience.professional_id  GROUP  BY `experience`.`early_success` DESC ";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Prepare();
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{

				ProfessionalViewModel ProfessionalVM = new ProfessionalViewModel(
					new Professional(int.Parse(reader["id_professional"].ToString()), int.Parse(reader["siret"].ToString()), reader["firstname"].ToString(), reader["lastname"].ToString(), reader["project_qualification"].ToString(), reader["project_datail"].ToString(), reader["availability"].ToString(), double.Parse(reader["price"].ToString()), reader["success"].ToString(), reader["email"].ToString(), reader["password"].ToString(), reader["avatar"].ToString()),
					new Services(int.Parse(reader["id_services"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["service"].ToString(), reader["service_details"].ToString(), reader["brochure"].ToString(), reader["images"].ToString()),
					new Experience(int.Parse(reader["id_experience"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["projet_carried"].ToString(), double.Parse(reader["early_success"].ToString()), double.Parse(reader["satisfaction_rate"].ToString()))) ;
				//INSERT INTO `experience` (`id_experience`, `professional_id`, `projet_carried`, `early_success`, `satisfaction_rate`) VALUES (NULL, '3', 'Design project manager', '512.5', '8.5')
				colProfessionalVM.Add(ProfessionalVM);
			}
			reader.Close();
			return colProfessionalVM;

		}

		public Experience getByIdProfessional(int id)
		{
			 Experience obj = new Experience();
			string query = "select * from Experience WHERE professional_id=@id";
			MySqlCommand command = new MySqlCommand(query, Database.connection);

			command.Parameters.AddWithValue("@id", id);
			command.Prepare();

			MySqlDataReader reader = command.ExecuteReader();
			//reader.Read();
			while (reader.Read())
			{
				 obj = new Experience(int.Parse(reader["id_experience"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["projet_carried"].ToString(), double.Parse(reader["early_success"].ToString()), double.Parse(reader["satisfaction_rate"].ToString()));
			}
			reader.Close();
			return obj;


		}
		public void save(Experience obj)
        {
            if (obj.Id_experience == 0)
            {
                string query = "INSERT INTO Experience (professional_id, projet_carried, early_success, satifaction) VALUES(@professional_id, @projet_carried, @early_success, @satifaction)";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@professional_id", obj.Professional_id);
                command.Parameters.AddWithValue("@projet_carried", obj.Projet_carried);
                command.Parameters.AddWithValue("@early_success", obj.Early_success);
                command.Parameters.AddWithValue("@satifaction", obj.Satifaction);


                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("INSERT OK");
            }
            else
            {
                string query = "UPDATE Experience SET professional_id=@professional_id, projet_carried=@projet_carried, early_success=@early_success, satifaction=@satifaction  WHERE id_experience=@id_experience";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@professional_id", obj.Professional_id);
                command.Parameters.AddWithValue("@projet_carried", obj.Projet_carried);
                command.Parameters.AddWithValue("@early_success", obj.Early_success);
                command.Parameters.AddWithValue("@satifaction", obj.Satifaction);
                command.Parameters.AddWithValue("@id_experience", obj.Id_experience);
                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("UPDATE OK");
            }
        }

        public void delete(int Id_experience)
        {
            string query = "DELETE FROM Experience WHERE id_experience=@id_experience";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Parameters.AddWithValue("@id_experience", Id_experience);
            command.Prepare();
            command.ExecuteNonQuery();
            Console.WriteLine("DELETE OK");

        }
    }
}
