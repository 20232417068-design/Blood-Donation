using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.Controllers
{
    public class HomeController : Controller
    {
        // ================= HOME PAGE =================
        public IActionResult Index()
        {
            ViewData["Title"] = "Home - Blood Donation";
            return View();
        }

        // ================= ABOUT PAGE =================
        public IActionResult About()
        {
            ViewData["Title"] = "About Us - Blood Donation";
            return View();
        }

        // ================= CONTACT PAGE =================
       
    }
}
