using MySql.Data.MySqlClient;

namespace Digital_info.Models
{
	public class ConversationDAO
	{
		/*public List<ConversationViewModel> GetAllConversations(int idEmetteur, int idRecepteur)
		{
			List<ConversationViewModel> conversations = new List<ConversationViewModel>();
			string query = @"
         SELECT
            conversation
            userEmetteur.pseudo AS idemmetteurpseudo,
            userRecepteur.pseudo AS idrecepteurspeudo,
            userEmetteur.photoDUtilisateur AS photoEmetteur,
            userRecepteur.photoDUtilisateur AS photoRecepteur
        FROM
            conversation,
            user AS userEmetteur,
            user AS userRecepteur
        WHERE
            conversation.idEmetteur = userEmetteur.id
            AND conversation.idRecepteur = userRecepteur.id
         AND ((idEmetteur = @idemmeteur AND idRecepteur = @idrecepteur) OR (idEmetteur = @idrecepteur AND idRecepteur = @idemmeteur))
    ORDER BY conversation.id;";
			MySqlCommand command = new MySqlCommand(query, Database.connection);
			command.Parameters.AddWithValue("@idemmeteur", idEmetteur);
			command.Parameters.AddWithValue("@idrecepteur", idRecepteur);
			command.Prepare();

			using (MySqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					ConversationViewModel conversation = new ConversationViewModel()
					{
						Id = int.Parse(reader["id"].ToString()),
						DteConversation = DateTime.Parse(reader["dteConversation"].ToString()),
						Message = reader["message"].ToString(),
						Idemetteur = int.Parse(reader["idEmetteur"].ToString()),
						Idrecepteur = int.Parse(reader["idRecepteur"].ToString()),
						IdemetteurSpeudo = reader["idemmetteurpseudo"].ToString(),
						IdrecepteurSpeudo = reader["idrecepteurspeudo"].ToString(),
						PhotoDUtilisateurEmetteur = reader["photoEmetteur"].ToString(),
						PhotoDUtilisateurRecepteur = reader["photoRecepteur"].ToString()
					};

					conversations.Add(conversation);
				}
			}

			return conversations;
		}*/
	}
}
