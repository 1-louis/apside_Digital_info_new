namespace Digital_info.Models
{
    public class Subscription
    {
        public int Id_subscription { get; set; }
        public int Id_client { get; set; }    
        public string Pays_mode { get; set; }
        public string Name_client { get; set; }
        public DateTime Start_date { get; set; }

        public DateTime End_date { get; set;}

        public double Price { get; set; }    

        public Subscription() { }

        public Subscription(int id_subscription, int id_client, string pays_mode, string name_client, DateTime start_date, DateTime end_date, double price)
        {
            Id_subscription = id_subscription;
            Id_client = id_client;
            Pays_mode = pays_mode;
            Name_client = name_client;
            Start_date = start_date;
            End_date = end_date;
            Price = price;
        }

        public Subscription(int id_client, string pays_mode, string name_client, DateTime start_date, DateTime end_date, double price)
        {
            Id_client = id_client;
            Pays_mode = pays_mode;
            Name_client = name_client;
            Start_date = start_date;
            End_date = end_date;
            Price = price;
        }
    }
}
