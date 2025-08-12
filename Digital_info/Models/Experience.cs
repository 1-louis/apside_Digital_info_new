namespace Digital_info.Models
{
    public class Experience
    {
        public int Id_experience { get; set; }    
        public int Professional_id { get; set; } 
        public string Projet_carried { get; set; }    
        public double Early_success { get; set; }
        public double Satifaction { get; set; }

		public Experience()
		{
		}
		public Experience(int id_experience, int professional_id, string projet_carried, double early_success, double satifaction)
		{
			Id_experience = id_experience;
			Professional_id = professional_id;
			Projet_carried = projet_carried;
			Early_success = early_success;
			Satifaction = satifaction;
		}

		public Experience(int professional_id, string projet_carried, double early_success, double satifaction)
		{
			Professional_id = professional_id;
			Projet_carried = projet_carried;
			Early_success = early_success;
			Satifaction = satifaction;
		}

		
	}
}
