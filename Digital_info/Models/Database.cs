using MySql.Data.MySqlClient;
using System.Data;

namespace Digital_info.Models
{
	public class Database
	{
		public static string connectionString = "server=localhost;database=digital_info;uid=root;pwd=\"\";";
		public static MySqlConnection connection = new MySqlConnection(connectionString);

		public static void Connect()
		{
			try
			{
				if (connection.State != ConnectionState.Open)
				{
					// je vérifie l'état de la connexion avant d'essayer de l'ouvrir
					connection.Open();
					Console.WriteLine("Connection Open ! ");

				}

			}
			catch (Exception ex)
			{
				Console.WriteLine("Can not open connection ! ");
			}
		}
		public static void Close()
		{
			connection.Close();
			Console.WriteLine("Connection Closed ! ");
		}




	}
}
