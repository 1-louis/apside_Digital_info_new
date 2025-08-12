using MySql.Data.MySqlClient;

namespace Digital_info.Models
{
    public class Appointment_DAO
    {
        public List<Appointment> getAll()
        {
            List<Appointment> col = new List<Appointment>();
            string query = "select * from appointment";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Prepare();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
            
                col.Add(new Appointment(int.Parse(reader["id_appointment"].ToString()), int.Parse(reader["Professional_id"].ToString()), int.Parse(reader["project_id"].ToString()), DateTime.Parse(reader["date"].ToString()), reader["appointment_titre"].ToString()));
            }
            reader.Close();
            return col;
        }

        public Appointment getById(int id)
        {
            Appointment obj = new Appointment();
            string query = "select * from appointment WHERE id_appointment=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);

            command.Parameters.AddWithValue("@id", id);
            command.Prepare();

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            obj = new Appointment(int.Parse(reader["id_appointment"].ToString()), int.Parse(reader["Professional_id"].ToString()), int.Parse(reader["project_id"].ToString()), DateTime.Parse(reader["date"].ToString()), reader["appointment_titre"].ToString());
            reader.Close();
            return obj;
        }
		public Appointment getByProjectId(int id)
		{
			Appointment obj = new Appointment();
			string query = "select * from appointment WHERE project_id=@id";
			MySqlCommand command = new MySqlCommand(query, Database.connection);

			command.Parameters.AddWithValue("@id", id);
			command.Prepare();

			MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
			{
				obj = new Appointment(int.Parse(reader["id_appointment"].ToString()), int.Parse(reader["Professional_id"].ToString()), int.Parse(reader["project_id"].ToString()), DateTime.Parse(reader["date"].ToString()), reader["appointment_titre"].ToString());

            }
            else
            {
                return obj;
            }

			reader.Close();
			return obj;
		}
		public void save(Appointment obj)
        {
            if (obj.Id_appointment == 0)
            {
                string query = "INSERT INTO appointment (professional_id, project_id, date, appointment_titre) VALUES(@professional_id,@project_id,@date,@appointment_titre)";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@professional_id", obj.Professional_id);
                command.Parameters.AddWithValue("@project_id", obj.Project_id);
                command.Parameters.AddWithValue("@date", obj.Date);
                command.Parameters.AddWithValue("@appointment_titre", obj.Appointment_titre);



                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("INSERT OK");
            }
            else
            {
                string query = "UPDATE appointment SET professional_id=@professional_id, project_id=@project_id, date=@date, appointment_titre=@appointment_titre WHERE id_appointment=@id_appointment";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@professional_id", obj.Professional_id);
                command.Parameters.AddWithValue("@project_id", obj.Project_id);
                command.Parameters.AddWithValue("@date", obj.Date);
                command.Parameters.AddWithValue("@appointment_titre", obj.Appointment_titre);
                command.Parameters.AddWithValue("@id_appointment", obj.Id_appointment);
                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("UPDATE OK");
            }
        }

        public void delete(int id)
        {
            string query = "DELETE FROM appointment WHERE id_appointment=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Parameters.AddWithValue("@id", id);
            command.Prepare();
            command.ExecuteNonQuery();
            Console.WriteLine("DELETE OK");

        }
    }
}
