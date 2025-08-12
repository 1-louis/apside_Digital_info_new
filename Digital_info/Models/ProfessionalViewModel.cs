namespace Digital_info.Models
{
	public class ProfessionalViewModel
	{
		public Professional Professional { get; set; }
		public Services Services { get; set; }
		public Experience Experience { get; set; }

		//public int Count { get; set; }
		public List<ProfessionalViewModel> ProfessionalVM { get; set; }

		public ProfessionalViewModel() { }

		public ProfessionalViewModel(Professional professional, Services services)
		{
			Professional = professional;
			Services = services;
		}
		public ProfessionalViewModel(Professional professional, Experience experience)
		{
			Professional = professional;
			Experience = experience;
		}

		public ProfessionalViewModel(Professional professional, Services services,
			Experience experience) : this(professional, services)
		{
			Experience = experience;
		}

		/*public ProfessionalViewModel(Professional professional)
{
	Professional = professional;
}*/





		/*	public ProfessionalViewModel(Professional Professional, Experience Experience, Services Services)
			{
				Sub_cat = sub_cat;
				Count = count;
			}*/

	}
}
