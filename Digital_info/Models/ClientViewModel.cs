using Stripe;

namespace Digital_info.Models
{
	public class ClientViewModel
	{
		public Client Client { get; set; }
		public Project Project { get; set; }

		public  Appointment Appointment { get; set; }	

		public Interest Interest { get; set; }	
		public Subscription	Subscription { get; set;}
		public List<Professional> ProfessionalVM { get; set; }

		public ClientViewModel()
		{
		}


		public ClientViewModel(Client client, Project project, Appointment appointment, Interest interest, Subscription subscription)
		{
			Client = client;
			Project = project;
			Appointment = appointment;
			Interest = interest;
			Subscription = subscription;
		}

		public ClientViewModel(Client client, Project project, Appointment appointment, Subscription subscription)
		{
			Client = client;
			Project = project;
			Appointment = appointment;
			Subscription = subscription;
		}

		public ClientViewModel(Client client, Project project, Appointment appointment)
		{
			Client = client;
			Project = project;
			Appointment = appointment;
		}

		public ClientViewModel(Client client, Project project, Appointment appointment, List<Professional> professionalVM) :
			this(client, project, appointment)
		{
			ProfessionalVM = professionalVM;
		}

		public ClientViewModel(Client client, Subscription subscription, Interest interest)
		{
			Client = client;
			Subscription = subscription;
			Interest = interest;
		}

		
	}
}
