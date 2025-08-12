using MySql.Data.MySqlClient;
using System.Net.Sockets;
using System.Xml.Linq;

namespace Digital_info.Models
{
    public class Project_DAO
    {
        public List<Project> getAll()
        {
            List<Project> col = new List<Project>();
            string query = "select * from Project";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Prepare();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                col.Add(new Project(int.Parse(reader["id_project"].ToString()), int.Parse(reader["client_id"].ToString()), reader["number"].ToString(), reader["type"].ToString(), reader["name"].ToString(), reader["estimates"].ToString(), int.Parse(reader["nbr_person"].ToString()), reader["siret"].ToString(), reader["description"].ToString()));
            }
            reader.Close();
            return col;
        }

        public Project getById(int id)
        {
            Project obj = new Project();
            string query = "select * from Project WHERE id_project=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);

            command.Parameters.AddWithValue("@id", id);
            command.Prepare();

            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read()) {
				obj = new Project(int.Parse(reader["id_project"].ToString()), int.Parse(reader["client_id"].ToString()), reader["number"].ToString(), reader["type"].ToString(), reader["name"].ToString(), reader["estimates"].ToString(), int.Parse(reader["nbr_person"].ToString()), reader["siret"].ToString(), reader["description"].ToString());
            }
            else
            {
                return obj;
            }
           

			reader.Close();
            return obj;


        }
		public List<Project> getByClientId(int id)
		{
			List<Project> col = new List<Project>();
			string query = "SELECT * FROM `project`WHERE client_id=@id";
			MySqlCommand command = new MySqlCommand(query, Database.connection);

			command.Parameters.AddWithValue("@id", id);
			command.Prepare();

			MySqlDataReader reader = command.ExecuteReader();
          

			while (reader.Read())
			{
				col.Add(new Project(int.Parse(reader["id_project"].ToString()), int.Parse(reader["client_id"].ToString()), reader["number"].ToString(), reader["type"].ToString(), reader["name"].ToString(), reader["estimates"].ToString(), int.Parse(reader["nbr_person"].ToString()), reader["siret"].ToString(), reader["description"].ToString()));
			}

			reader.Close();
			return col;


		}
		public void save(Project obj)
        {
            if (obj.Id_project == 0)
            {
                string query = "INSERT INTO `project` ( `client_id`, `number`, `type`, `name`, `estimates`, `nbr_person`, `siret`, `description`) VALUES (@client_id, @number, @type, @name, @estimates, @nbr_person, @siret, @description )";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@client_id", obj.Id_client);
                command.Parameters.AddWithValue("@number", obj.Number);
                command.Parameters.AddWithValue("@type", obj.Type);
                command.Parameters.AddWithValue("@name", obj.Name);
                command.Parameters.AddWithValue("@estimates", obj.Estimates);
                command.Parameters.AddWithValue("@nbr_person", obj.Nbr_peron);
				command.Parameters.AddWithValue("@siret", obj.Siret);
				command.Parameters.AddWithValue("@description", obj.Description);

				command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("INSERT OK");
            }
            else
            {
                string query = "UPDATE Project SET client_id=@client_id, number=@number, type=@type, name=@name, estimates=@estimates, nbr_person=@nbr_person, siret=@siret, description=@description WHERE id_project=@id_project";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@client_id", obj.Id_client);
                command.Parameters.AddWithValue("@number", obj.Number);
                command.Parameters.AddWithValue("@type", obj.Type);
                command.Parameters.AddWithValue("@name", obj.Name);
                command.Parameters.AddWithValue("@estimates", obj.Estimates);
                command.Parameters.AddWithValue("@nbr_person", obj.Nbr_peron);
                command.Parameters.AddWithValue("@siret", obj.Siret);
                command.Parameters.AddWithValue("@description", obj.Description);
                command.Parameters.AddWithValue("@id_project", obj.Id_project);
                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("UPDATE OK");
            }
        }

        public void delete(int id)
        {
            string query = "DELETE FROM Project WHERE id_project=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Parameters.AddWithValue("@id", id);
            command.Prepare();
            command.ExecuteNonQuery();
            Console.WriteLine("DELETE OK");

        }
    }
}
