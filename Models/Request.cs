namespace BloodDonation.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BloodGroup { get; set; }
        public string City { get; set; }
        public string ContactNumber { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;
    }
}
