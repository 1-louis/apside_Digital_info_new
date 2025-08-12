using MySql.Data.MySqlClient;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Digital_info.Models
{
    public class Services_DAO
    {
        public List<Services> getAll()
        {
            List<Services> col = new List<Services>();
            string query = "select * from services ";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Prepare();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                col.Add(new Services(int.Parse(reader["id_services"].ToString()), int.Parse(reader["professional_id"].ToString()),reader["service"].ToString(), reader["service_details"].ToString(), reader["brochure"].ToString(), reader["images"].ToString()));
            }
            reader.Close();
            return col;
        }

        public Services getById(int id)
        {
            Services obj = new Services();
            string query = "select * from Services WHERE id_services=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);

            command.Parameters.AddWithValue("@id", id);
            command.Prepare();

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            obj = new Services(int.Parse(reader["id_services"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["service"].ToString(), reader["service_details"].ToString(), reader["brochure"].ToString(), reader["images"].ToString());
            reader.Close();
            return obj;


        }
		public Services getByIdProf(int id)
		{
			Services obj = new Services();
			string query = "select * from Services WHERE professional_id=@id";
			MySqlCommand command = new MySqlCommand(query, Database.connection);

			command.Parameters.AddWithValue("@id", id);
			command.Prepare();

			MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
				obj = new Services(int.Parse(reader["id_services"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["service"].ToString(), reader["service_details"].ToString(), reader["brochure"].ToString(), reader["images"].ToString());

            }
            else
            {
                return obj; 
            }
          

			reader.Close();
			return obj;


		}
		
		public int GetCountAll()
		{
			string query = "SELECT COUNT(*) FROM services";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Prepare();
			return Convert.ToInt32(command.ExecuteScalar());
		}
		public List<ProfessionalViewModel> getIdProfessionalVM(int id,  int safeLimit)
		{
			//SELECT COUNT(id) as count FROM ad WHERE id_sub_category in (SELECT id FROM sub_category WHERE id_category in(SELECT id FROM category WHERE id =@id))
			//SELECT category.id, category.title, category.picture, COUNT(*)FROM category,  sub_category, ad WHERE  category.id = sub_category.id_category AND sub_category.id = ad.id_sub_category GROUP BY category.id
			List<ProfessionalViewModel> colProfessionalVM = new List<ProfessionalViewModel>();
			string query = "SELECT professionnal.*, services.*, experience.* FROM services,professionnal LEFT JOIN experience ON professionnal.id_professional = experience.professional_id WHERE professionnal.id_professional=services.professional_id and professionnal.id_professional=@id LIMIT "+safeLimit;
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Parameters.AddWithValue("@id", id);
			//command.Parameters.AddWithValue("@safeLimit", safeLimit);



			command.Prepare();
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{

				ProfessionalViewModel ProfessionalVM = new ProfessionalViewModel(
					new Professional(int.Parse(reader["id_professional"].ToString()), int.Parse(reader["siret"].ToString()), reader["firstname"].ToString(), reader["lastname"].ToString(), reader["project_qualification"].ToString(), reader["project_datail"].ToString(), reader["availability"].ToString(), double.Parse(reader["price"].ToString()), reader["success"].ToString(), reader["email"].ToString(), reader["password"].ToString(), reader["avatar"].ToString()),
					new Services(int.Parse(reader["id_services"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["service"].ToString(), reader["service_details"].ToString(), reader["brochure"].ToString(), reader["images"].ToString()),
					new Experience(int.Parse(reader["id_experience"].ToString()), int.Parse(reader["professional_id"].ToString()), reader["projet_carried"].ToString(), double.Parse(reader["early_success"].ToString()), double.Parse(reader["satisfaction_rate"].ToString()))) ;

				colProfessionalVM.Add(ProfessionalVM);
			}
			reader.Close();
			return colProfessionalVM;

		}
		public void save(Services obj)
        {
            if (obj.Id_services == 0)
            {
                string query = "INSERT INTO Services (professional_id, service, service_details, brochure, images) VALUES(@professional_id, @service,@service_details,@brochure,@images)";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@professional_id", obj.Professional_id);
                command.Parameters.AddWithValue("@service", obj.Service);
                command.Parameters.AddWithValue("@service_details", obj.Service_details);

				command.Parameters.AddWithValue("@brochure", obj.Brochure);
				command.Parameters.AddWithValue("@images", obj.Images);


				command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("INSERT OK");
            }
            else
            {
                string query = "UPDATE Services SET professional_Id=@professional_Id, service=@service, service_details=@service_details,brochure=@brochure,images=@images WHERE id_services=@id_services";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@professional_id", obj.Professional_id);
                command.Parameters.AddWithValue("@service", obj.Service);
                command.Parameters.AddWithValue("@service_details", obj.Service_details);
				command.Parameters.AddWithValue("@brochure", obj.Brochure);
				command.Parameters.AddWithValue("@images", obj.Images);

				command.Parameters.AddWithValue("@id_services", obj.Id_services);
                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("UPDATE OK");
            }
        }

        public void delete(int id)
        {
            string query = "DELETE FROM Services WHERE id_services=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Parameters.AddWithValue("@id", id);
            command.Prepare();
            command.ExecuteNonQuery();
            Console.WriteLine("DELETE OK");

        }

	
	}
}
