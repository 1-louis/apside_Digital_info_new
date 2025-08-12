namespace Digital_info.Models
{
    public class Images
    {
        public int Id_images { get; set; }
        public int Id_client { get; set; }
        public string Image { get; set; } 
        public string Title { get; set; }  
        
        public Images() { }

        public Images(int id_images, int id_client, string image, string title)
        {
            Id_images = id_images;
            Id_client = id_client;
            Image = image;
            Title = title;
        }

        public Images(int id_client, string image, string title)
        {
            Id_client = id_client;
            Image = image;
            Title = title;
        }
    }
}
