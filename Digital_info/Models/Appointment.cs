namespace Digital_info.Models
{
    public class Appointment
    {
       public int Id_appointment { get; set; } 
        public int Professional_id { get; set; } 
        public int Project_id { get; set; }

        public DateTime Date { get; set; }

        public string Appointment_titre { get; set; }

        public Appointment() { }

		public Appointment(int id_appointment, int professional_id, int project_id, DateTime date, string appointment_titre)
		{
			Id_appointment = id_appointment;
			Professional_id = professional_id;
			Project_id = project_id;
			Date = date;
			Appointment_titre = appointment_titre;
		}

		public Appointment(int professional_id, int project_id, DateTime date, string appointment_titre)
		{
			Professional_id = professional_id;
			Project_id = project_id;
			Date = date;
			Appointment_titre = appointment_titre;
		}
	}
}
