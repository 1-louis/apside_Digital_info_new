namespace Digital_info.Models
{
    public class Services
    {
        public int Id_services {get ; set;}

        public int Professional_id { get; set; }

        public string Service { get; set;}
        public string Service_details { get; set;}
        public string Brochure { get; set; }
        public string Images { get; set; }


        public Services() { }

		public Services(int id_services, int professional_id, string service, string service_details, string brochure, string images)
		{
			Id_services = id_services;
			Professional_id = professional_id;
			Service = service;
			Service_details = service_details;
			Brochure = brochure;
			Images = images;
		}

		public Services(int professional_id, string service, string service_details, string brochure, string images)
		{
			Professional_id = professional_id;
			Service = service;
			Service_details = service_details;
			Brochure = brochure;
			Images = images;
		}
	}
}
