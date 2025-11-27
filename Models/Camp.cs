namespace BloodDonation.Models
{
    public class Camp
    {
        public int Id { get; set; }
        public string CampName { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }

        // Make optional
        public string? Description { get; set; }
    }
}
