namespace Digital_info.Models
{
    public class Interest
    {
        public int Id_interest { get; set; }
        public int Id_client { get; set; }
		public string Type_project { get; set; }
		public int Nbr_peson { get; set; }
		public string Document { get; set; }
		public string Description { get; set; }    
        public double Price { get; set; }    

        public Interest() { }

		public Interest(int id_interest, int id_client, string type_project, int nbr_peson,  string description, double price)
		{
			Id_interest = id_interest;
			Id_client = id_client;
			Type_project = type_project;
			Nbr_peson = nbr_peson;
/*			Document = document;
*/			Description = description;
			Price = price;
		}

	/*	public Interest(int id_client, string type_project, int nbr_peson, string document, string description, double price)
		{
			Id_client = id_client;
			Type_project = type_project;
			Nbr_peson = nbr_peson;
			Document = document;
			Description = description;
			Price = price;
		}*/

		public Interest(int id_client, string type_project, int nbr_peson, string description, double price)
		{
			Id_client = id_client;
			Type_project = type_project;
			Nbr_peson = nbr_peson;
			Description = description;
			Price = price;
		}
	}
}
