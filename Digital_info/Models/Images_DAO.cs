using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Sockets;

namespace Digital_info.Models
{
    public class Images_DAO
    {
        public List<Images> getAll()
        {
            List<Images> col = new List<Images>();
            string query = "select * from Images";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Prepare();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                col.Add(new Images(int.Parse(reader["Id_images"].ToString()), int.Parse(reader["id_client"].ToString()), reader["image"].ToString(), reader["title"].ToString()));
            }
            reader.Close();
            return col;
        }

        public Images getById(int id)
        {
            Images obj = new Images();
            string query = "select * from Images WHERE id=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);

            command.Parameters.AddWithValue("@id", id);
            command.Prepare();

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            obj = new Images(int.Parse(reader["Id_images"].ToString()), int.Parse(reader["id_client"].ToString()), reader["image"].ToString(), reader["title"].ToString());
            reader.Close();
            return obj;


        }

        public void save(Images obj)
        {
            if (obj.Id_images == 0)
            {
                string query = "INSERT INTO Images ( id_client, image, title ) VALUES( @id_client, @image, @title )";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@id_client", obj.Id_client);
                command.Parameters.AddWithValue("@image", obj.Image);
                command.Parameters.AddWithValue("@title", obj.Title);


                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("INSERT OK");
            }
            else
            {
                string query = "UPDATE Images SET id_client=@id_client, image=@image, title=@title WHERE id_images=@id_images";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@id_client", obj.Id_client);
                command.Parameters.AddWithValue("@image", obj.Image);
                command.Parameters.AddWithValue("@title", obj.Title);


                command.Parameters.AddWithValue("@id_images", obj.Id_images);
                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("UPDATE OK");
            }
        }

        public void delete(int id)
        {
            string query = "DELETE FROM Images WHERE id_images=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Parameters.AddWithValue("@id", id);
            command.Prepare();
            command.ExecuteNonQuery();
            Console.WriteLine("DELETE OK");

        }
    }
}
