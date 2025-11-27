namespace BloodDonation.Models
{
    public class Donor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }     // ✔ Required by your form

        public string BloodGroup { get; set; }

        public string Phone { get; set; }

        public string City { get; set; }
    }
}
