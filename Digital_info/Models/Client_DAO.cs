using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Digital_info.Models
{
    public class Client_DAO
    {
        public List<Client> getAll()
        {
            List<Client> col = new List<Client>();
            string query = "select * from client";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Prepare();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
         
                col.Add(new Client(int.Parse(reader["id_client"].ToString()), reader["firstname"].ToString(),
					reader["lastname"].ToString(), reader["address"].ToString(), reader["city"].ToString(), reader["phone"].ToString(), 
					int.Parse(reader["age"].ToString()), reader["email"].ToString(), reader["password"].ToString()));
            }
            reader.Close();
            return col;
        }

        public Client getById(int id)
        {
            Client obj = new Client();
            string query = "select * from client WHERE id_client=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);

            command.Parameters.AddWithValue("@id", id);
            command.Prepare();

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            obj = new Client(int.Parse(reader["id_client"].ToString()), reader["firstname"].ToString(),reader["lastname"].ToString(),reader["address"].ToString(),reader["city"].ToString(),reader["phone"].ToString(),int.Parse(reader["age"].ToString()),reader["email"].ToString());
            reader.Close();
            return obj;


        }
		public List<ClientViewModel> getByIdClientVM(int id)
		{
			List<ClientViewModel> colClientVM = new List<ClientViewModel>();
			string query = "SELECT * FROM client, project,appointment,subscription WHERE client.id_client=project.client_id AND project.id_project=appointment.project_id  AND id_client=@id GROUP BY appointment.date DESC";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Parameters.AddWithValue("@id", id);

			command.Prepare();
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{

				ClientViewModel ClientVM = new ClientViewModel(
				 new Client(int.Parse(reader["id_client"].ToString()), reader["firstname"].ToString(), reader["lastname"].ToString(), reader["address"].ToString(), reader["city"].ToString(), reader["phone"].ToString(), int.Parse(reader["age"].ToString()), reader["email"].ToString(), reader["password"].ToString()),
				 new Project(int.Parse(reader["id_project"].ToString()), int.Parse(reader["client_id"].ToString()), reader["number"].ToString(), reader["type"].ToString(), reader["name"].ToString(), reader["estimates"].ToString(), int.Parse(reader["nbr_person"].ToString()), reader["siret"].ToString(), reader["description"].ToString()),
				 new Appointment(int.Parse(reader["id_appointment"].ToString()), int.Parse(reader["Professional_id"].ToString()), int.Parse(reader["project_id"].ToString()), DateTime.Parse(reader["date"].ToString()), reader["appointment_titre"].ToString()));


				colClientVM.Add(ClientVM);
			}
			reader.Close();
			return colClientVM;

		}
		public List<ClientViewModel> getByAllIdClient(int id)
		{
			ClientViewModel ClientVM = new ClientViewModel();

			List<ClientViewModel> colClientVM = new List<ClientViewModel>();
			string query = "SELECT * FROM client, project, subscription LEFT JOIN appointment ON appointment." +
				"project_id WHERE project.client_id= client.id_client AND client.id_client = subscription.client_id GROUP BY project.client_id=@id";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Parameters.AddWithValue("@id", id);

			command.Prepare();
			MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{

				ClientVM.Client = new Client(int.Parse(reader["id_client"].ToString()), reader["firstname"].ToString(), reader["lastname"].ToString(), reader["address"].ToString(), reader["city"].ToString(), reader["phone"].ToString(), int.Parse(reader["age"].ToString()), reader["email"].ToString(), reader["password"].ToString());
				ClientVM.Project = new Project(int.Parse(reader["id_project"].ToString()), int.Parse(reader["client_id"].ToString()), reader["number"].ToString(), reader["type"].ToString(), reader["name"].ToString(), reader["estimates"].ToString(), int.Parse(reader["nbr_person"].ToString()), reader["siret"].ToString(), reader["description"].ToString());
				ClientVM.Appointment = new Appointment(int.Parse(reader["id_appointment"].ToString()), int.Parse(reader["Professional_id"].ToString()), int.Parse(reader["project_id"].ToString()), DateTime.Parse(reader["date"].ToString()), reader["appointment_titre"].ToString());
			
				colClientVM.Add(ClientVM);
			}
			reader.Close();
			foreach (ClientViewModel colCli in colClientVM)
			{
				colCli.ProfessionalVM = new Professional_DAO(). getByIdVM(colCli.Appointment.Professional_id);
			}
			return colClientVM;

		}
		public List<ClientViewModel> getByEmailClient(string email)
		{
			List<ClientViewModel> colClientVM = new List<ClientViewModel>();
			string query = "SELECT * FROM client,subscription,interest WHERE client.id_client=subscription.client_id  " +
				"AND interest.client_id=client.id_client  AND client.email=@email";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Parameters.AddWithValue("@email", email);

			command.Prepare();
			MySqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{

				ClientViewModel ClientVM = new ClientViewModel(
				 new Client(int.Parse(reader["id_client"].ToString()), reader["firstname"].ToString(), reader["lastname"].ToString(), reader["address"].ToString(), reader["city"].ToString(), reader["phone"].ToString(), int.Parse(reader["age"].ToString()), reader["email"].ToString(), reader["password"].ToString()),
				 new Subscription(int.Parse(reader["id_subscription"].ToString()), int.Parse(reader["client_id"].ToString()), reader["pay_mode"].ToString(), reader["name_client"].ToString(), DateTime.Parse(reader["start_date"].ToString()), DateTime.Parse(reader["end_date"].ToString()), double.Parse(reader["price"].ToString())),

				new Interest(int.Parse(reader["id_interest"].ToString()), int.Parse(reader["id_client"].ToString()), reader["type_project"].ToString(), int.Parse(reader["nbr_peson"].ToString()), reader["description"].ToString(), double.Parse(reader["price"].ToString())));


				colClientVM.Add(ClientVM);
			}
			reader.Close();
			return colClientVM;

		}
		public int GetCountAll()
		{
			string query = "SELECT COUNT(*) FROM client";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Prepare();
			return Convert.ToInt32(command.ExecuteScalar());
		}
		public Client login(string email, string password)
		{
			Client obj = new Client();
			string query = "select * from client WHERE email=@email";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Parameters.AddWithValue("@email", email);
			command.Prepare();

			MySqlDataReader reader = command.ExecuteReader();
			if (reader.Read())
			{
				if (BCrypt.Net.BCrypt.Verify(password, reader["password"].ToString()))
				{
					obj = new Client(int.Parse(reader["id_client"].ToString()), reader["firstname"].ToString(), reader["lastname"].ToString(), reader["address"].ToString(), reader["city"].ToString(), reader["phone"].ToString(), int.Parse(reader["age"].ToString()), reader["email"].ToString(), reader["password"].ToString()); 
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
		public void savePassword(Client obj)
		{

			string query = "UPDATE client SET  password=@password WHERE id_client=@id_client";
			MySqlCommand command = new MySqlCommand(query, Database.connection);

			command.Parameters.AddWithValue("@password", obj.Password);
			command.Parameters.AddWithValue("@id_client", obj.Id_client);

			command.Prepare();
			command.ExecuteNonQuery();
			Console.WriteLine("UPDATE OK");

		}
		
		public void save(Client obj)
        {
            if (obj.Id_client == 0)
            {
                string query = "INSERT INTO client (  firstname, lastname, address, city, phone, age, email, password) VALUES ( @firstname,@lastname,@address,@city,@phone,@age,@email,@password)";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@firstname", obj.Firstname);
                command.Parameters.AddWithValue("@lastname", obj.Lastname);
                command.Parameters.AddWithValue("@address", obj.Address);
                command.Parameters.AddWithValue("@city", obj.City);
                command.Parameters.AddWithValue("@phone", obj.Phone);
                command.Parameters.AddWithValue("@age", obj.Age);
                command.Parameters.AddWithValue("@email", obj.Email);
                command.Parameters.AddWithValue("@password", obj.Password);
                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("INSERT OK");
            }
            else if (obj.Password != null)
            {
				string query = "UPDATE client SET firstname=@firstname, lastname=@lastname, address=@address, city=@city, phone=@phone, age=@age, email=@email, password=@password WHERE id_client=@id_client";
				MySqlCommand command = new MySqlCommand(query, Database.connection);
				command.Parameters.AddWithValue("@firstname", obj.Firstname);
				command.Parameters.AddWithValue("@lastname", obj.Lastname);
				command.Parameters.AddWithValue("@address", obj.Address);
				command.Parameters.AddWithValue("@city", obj.City);
				command.Parameters.AddWithValue("@phone", obj.Phone);
				command.Parameters.AddWithValue("@age", obj.Age);
				command.Parameters.AddWithValue("@email", obj.Email);
				command.Parameters.AddWithValue("@password", obj.Password);
				command.Parameters.AddWithValue("@id_client", obj.Id_client);



				command.Prepare();
				command.ExecuteNonQuery();
				Console.WriteLine("UPDATE OK");
			}
			else
			{
                string query = "UPDATE client SET firstname=@firstname, lastname=@lastname, address=@address, city=@city, phone=@phone, age=@age, email=@email WHERE id_client=@id_client";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@firstname", obj.Firstname);
                command.Parameters.AddWithValue("@lastname", obj.Lastname);
                command.Parameters.AddWithValue("@address", obj.Address);
                command.Parameters.AddWithValue("@city", obj.City);
                command.Parameters.AddWithValue("@phone", obj.Phone);
                command.Parameters.AddWithValue("@age", obj.Age);
                command.Parameters.AddWithValue("@email", obj.Email);
                command.Parameters.AddWithValue("@id_client", obj.Id_client);

                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("UPDATE OK");
            }
        }

        public void delete(int id)
        {
            string query = "DELETE FROM client WHERE id_client=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Parameters.AddWithValue("@id", id);
            command.Prepare();
            command.ExecuteNonQuery();
            Console.WriteLine("DELETE OK");

        }
    }
}
