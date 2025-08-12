using MySql.Data.MySqlClient;
using System.Net.Sockets;

namespace Digital_info.Models
{
    public class Interest_DAO
    {
        public List<Interest> getAll()
        {
            List<Interest> col = new List<Interest>();
            string query = "select * from Interest";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Prepare();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                col.Add(new Interest(int.Parse(reader["id_interest"].ToString()), int.Parse(reader["client_id"].ToString()), reader["type_project"].ToString(), int.Parse(reader["nbr_peson"].ToString()), reader["description"].ToString(), double.Parse(reader["price"].ToString())));
            }
            reader.Close();
            return col;
        }

        public Interest getById(int id)
        {
            Interest obj = new Interest();
            string query = "select * from Interest WHERE id_interest=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);

            command.Parameters.AddWithValue("@id", id);
            command.Prepare();

            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {

				obj = new Interest(int.Parse(reader["id_interest"].ToString()), int.Parse(reader["client_id"].ToString()), reader["type_project"].ToString(), int.Parse(reader["nbr_peson"].ToString()), reader["description"].ToString(), double.Parse(reader["price"].ToString()));
				reader.Close();
            }
           

            return obj;


        }

        public void save(Interest obj)
        {
            if (obj.Id_interest == 0)
            {
                string query = "INSERT INTO Interest (client_id,type_project, description, nbr_peson,price) VALUES(@client_id, @type_project, @description, @nbr_peson,@price)";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@client_id", obj.Id_client);
				command.Parameters.AddWithValue("@type_project", obj.Type_project);
				command.Parameters.AddWithValue("@description", obj.Description);
                command.Parameters.AddWithValue("@nbr_peson", obj.Nbr_peson);
                command.Parameters.AddWithValue("@price", obj.Price);



                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("INSERT OK");
            }
            else
            {
                string query = "UPDATE Interest SET client_id=@client_id,type_project=@type_project, description=@description, nbr_peson=@nbr_peson, price=@price WHERE id_interest=@id_interest";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@client_id", obj.Id_client);
				command.Parameters.AddWithValue("@type_project", obj.Type_project);
				command.Parameters.AddWithValue("@description", obj.Description);
                command.Parameters.AddWithValue("@nbr_peson", obj.Nbr_peson);
                command.Parameters.AddWithValue("@price", obj.Price);
                command.Parameters.AddWithValue("@id_interest", obj.Id_interest);
                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("UPDATE OK");
            }
        }

        public void delete(int id)
        {
            string query = "DELETE FROM Interest WHERE id_interest=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Parameters.AddWithValue("@id", id);
            command.Prepare();
            command.ExecuteNonQuery();
            Console.WriteLine("DELETE OK");

        }
    }
}
