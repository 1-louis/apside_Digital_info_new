namespace Digital_info.Models
{
    public class Estimate
    {
        public int Id_estimate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public string Type { get; set; }

        public Estimate(int id_estimate, string name, string description, string type)
        {
            Id_estimate = id_estimate;
            Name = name;
            Description = description;
            Type = type;
        }

        public Estimate(string name, string description, string type)
        {
            Name = name;
            Description = description;
            Type = type;
        }
    }
}
