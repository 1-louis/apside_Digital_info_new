namespace Digital_info.Models
{
    public class Project
    {
        public int Id_project { get; set; } 
        public int Id_client { get; set; }    
        public string Number { get; set; } 
        public string Type { get; set; }
        public string Name { get; set; } 
        public string Estimates { get; set; } 

        public int Nbr_peron { get; set; } 

        public string Siret { get; set; }
        public string Description { get; set; } 
        public Project() { }

        public Project(int id_project, int id_client, string number, string type, string name, string estimates, int nbr_peron, string siret, string description)
        {
            Id_project = id_project;
            Id_client = id_client;
            Number = number;
            Type = type;
            Name = name;
            Estimates = estimates;
            Nbr_peron = nbr_peron;
            Siret = siret;
            Description = description;
        }

        public Project(int id_client, string number, string type, string name, string estimates, int nbr_peron, string siret, string description)
        {
            Id_client = id_client;
            Number = number;
            Type = type;
            Name = name;
            Estimates = estimates;
            Nbr_peron = nbr_peron;
            Siret = siret;
            Description = description;
        }

    }
}
