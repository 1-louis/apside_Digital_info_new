using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Net.Sockets;

namespace Digital_info.Models
{
    public class Subscription_DAO
    {
        public List<Subscription> getAll()
        {
            List<Subscription> col = new List<Subscription>();
            string query = "select * from Subscription";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Prepare();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
             
                col.Add(new Subscription(int.Parse(reader["id_subscription"].ToString()), int.Parse(reader["id_client"].ToString()), reader["pay_mode"].ToString(),reader["name_client"].ToString(), DateTime.Parse(reader["start_date"].ToString()), DateTime.Parse(reader["end_date"].ToString()), double.Parse(reader["price"].ToString())));
            }
            reader.Close();
            return col;
        }

        public Subscription getById(int id)
        {
            Subscription obj = new Subscription();
            string query = "select * from Subscription WHERE id=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);

            command.Parameters.AddWithValue("@id", id);
            command.Prepare();

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();

            obj = new Subscription(int.Parse(reader["id_subscription"].ToString()), int.Parse(reader["id_client"].ToString()), reader["pay_mode"].ToString(), reader["name_client"].ToString(), DateTime.Parse(reader["start_date"].ToString()), DateTime.Parse(reader["end_date"].ToString()), double.Parse(reader["price"].ToString()));
            reader.Close();
            return obj;


        }

		public Subscription getSubcriptionByEmailClient(string email)
		{
			Subscription obj = new Subscription();
			string query = "select * from subscription  WHERE name_client=@email";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Parameters.AddWithValue("@email", email);
			command.Prepare();

			MySqlDataReader reader = command.ExecuteReader();
			if (reader.Read())
			{
			
					obj = new Subscription(int.Parse(reader["id_subscription"].ToString()), int.Parse(reader["client_id"].ToString()), reader["pay_mode"].ToString(), reader["name_client"].ToString(), DateTime.Parse(reader["start_date"].ToString()), DateTime.Parse(reader["end_date"].ToString()), double.Parse(reader["price"].ToString()));
				
				
			}
			else
			{
                
				return obj;
			}
			reader.Close();
			return obj;
		}
        public void save(Subscription obj)
        {
            if (obj.Id_subscription == 0)
            {
				
                string query = "INSERT INTO subscription ( client_id, pay_mode, name_client, start_date, end_date, price)  VALUES(@client_id, @pay_mode, @name_client, @start_date, @end_date, @price)";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@client_id", obj.Id_client);
                command.Parameters.AddWithValue("@pay_mode", obj.Pays_mode);
                command.Parameters.AddWithValue("@name_client", obj.Name_client);
                command.Parameters.AddWithValue("@start_date", obj.Start_date);
                command.Parameters.AddWithValue("@end_date", obj.End_date);
                command.Parameters.AddWithValue("@price", obj.Price);

                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("INSERT OK");
            }
            else
            {
                string query = "UPDATE subscription SET client_id=@id_client, pay_mode=@pay_mode, name_client=@name_client, start_date=@start_date, end_date=@end_date, price=@price WHERE id_subscription=@id_subscription";
                MySqlCommand command = new MySqlCommand(query, Database.connection);
                command.Parameters.AddWithValue("@id_client", obj.Id_client);
                command.Parameters.AddWithValue("@pay_mode", obj.Pays_mode);
                command.Parameters.AddWithValue("@name_client", obj.Name_client);

                command.Parameters.AddWithValue("@start_date", obj.Start_date);
                command.Parameters.AddWithValue("@end_date", obj.End_date);
                command.Parameters.AddWithValue("@price", obj.Price);


                command.Parameters.AddWithValue("@id_subscription", obj.Id_subscription);
                command.Prepare();
                command.ExecuteNonQuery();
                Console.WriteLine("UPDATE OK");
            }
        }

        public void delete(int id)
        {
            string query = "DELETE FROM Subscription WHERE id_subscription=@id";
            MySqlCommand command = new MySqlCommand(query, Database.connection);
            command.Parameters.AddWithValue("@id", id);
            command.Prepare();
            command.ExecuteNonQuery();
            Console.WriteLine("DELETE OK");

        }
    }
}
