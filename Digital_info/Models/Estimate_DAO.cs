using MySql.Data.MySqlClient;

namespace Digital_info.Models
{
    public class Estimate_DAO
    {
        public List<Estimate> getAll()
        {
            List<Estimate> col = new List<Estimate>();
            string query = "select * from Estimate";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Prepare();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                col.Add(new Estimate(int.Parse(reader["id_estimate"].ToString()), reader["name"].ToString(), reader["description"].ToString(),reader["type"].ToString()));
            }
            reader.Close();
            return col;
        }

        public Estimate getById(int id)
        {
            //Estimate obj = new Estimate();
            string query = "select * from Estimate WHERE id=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);

            command.Parameters.AddWithValue("@id", id);
            command.Prepare();

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            Estimate obj = new Estimate(int.Parse(reader["id_estimate"].ToString()), reader["name"].ToString(), reader["description"].ToString(), reader["type"].ToString());
            reader.Close();
            return obj;


        }

        public void save(Estimate obj)
        {
            if (obj.Id_estimate == 0)
            {
                string query = "INSERT INTO Estimate (name, description, type) VALUES(@name, @description, @type)";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@name", obj.Name);
                command.Parameters.AddWithValue("@description", obj.Description);
                command.Parameters.AddWithValue("@type", obj.Type);


                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("INSERT OK");
            }
            else
            {
                string query = "UPDATE Estimate SET name=@name, description=@description, type=@type WHERE id_estimate=@id_estimate";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@name", obj.Name);
                command.Parameters.AddWithValue("@description", obj.Description);
                command.Parameters.AddWithValue("@type", obj.Type);
                command.Parameters.AddWithValue("@id_estimate", obj.Id_estimate);
                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("UPDATE OK");
            }
        }

        public void delete(int Id_estimate)
        {
            string query = "DELETE FROM Estimate WHERE id_estimate=@id_estimate";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Parameters.AddWithValue("@id_estimate", Id_estimate);
            command.Prepare();
            command.ExecuteNonQuery();
            Console.WriteLine("DELETE OK");

        }
    }
}
