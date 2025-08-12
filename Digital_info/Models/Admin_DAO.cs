using MySql.Data.MySqlClient;

namespace Digital_info.Models
{
    public class Admin_DAO
    {
        public List<Admin> getAll()
        {
            List<Admin> col = new List<Admin>();
            string query = "select * from admin";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Prepare();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                col.Add(new Admin(int.Parse(reader["id_admin"].ToString()), reader["email"].ToString(), reader["password"].ToString()));
            }
            reader.Close();
            return col;
        }
		public Admin login(string email, string password)
		{
			Admin obj;
			string query = "select * from admin WHERE email=@email";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Parameters.AddWithValue("@email", email);
			command.Prepare();

			MySqlDataReader reader = command.ExecuteReader();
			if (reader.Read())
			{
				if (BCrypt.Net.BCrypt.Verify(password, reader["password"].ToString()))
				{
					obj = new Admin(int.Parse(reader["id_admin"].ToString()) ,reader["email"].ToString(), reader["password"].ToString());
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
		public Admin getById(int id)
        {
            Admin obj = new Admin();
            string query = "select * from admin WHERE id=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);

            command.Parameters.AddWithValue("@id", id);
            command.Prepare();

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            obj = new Admin(int.Parse(reader["id_admin"].ToString()), reader["email"].ToString(), reader["password"].ToString());
            reader.Close();
            return obj;


        }

        public void save(Admin obj)
        {
            if (obj.Id_admin == 0)
            {
                string query = "INSERT INTO admin (email, password) VALUES(@email, @password)";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@email", obj.Email);
                command.Parameters.AddWithValue("@password", obj.Password);


                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("INSERT OK");
            }
            else
            {
                string query = "UPDATE admin SET email=@email, password=@password,  WHERE id=@id";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@email", obj.Email);
                command.Parameters.AddWithValue("@password", obj.Password);


                command.Parameters.AddWithValue("@id", obj.Id_admin);
                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("UPDATE OK");
            }
        }

        public void delete(int id)
        {
            string query = "DELETE FROM admin WHERE id=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Parameters.AddWithValue("@id", id);
            command.Prepare();
            command.ExecuteNonQuery();
            Console.WriteLine("DELETE OK");

        }
    }
}
