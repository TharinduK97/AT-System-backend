namespace hp_proj_1_backend.Models
{
    public class Cv
    {
        public int ID { get; set; }
        public string Cvpath { get; set; }
        public User User { get; set; }
        public int UserID { get; set; }

    }
}