using BloodDonation.Data;
using BloodDonation.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.Controllers
{
    public class DonorsController : Controller
    {
        private readonly AppDbContext _db;

        // ✅ Constructor (Fix for _db error)
        public DonorsController(AppDbContext db)
        {
            _db = db;
        }

        // ================= SHOW FORM =================
        public IActionResult Create()
        {
            return View();   // Loads Views/Donors/Create.cshtml
        }

        // ================= SAVE DONOR =================
        [HttpPost]
        public IActionResult Create(Donor model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _db.Donors.Add(model);
            _db.SaveChanges();

            TempData["Success"] = "Registration successful!";
            return RedirectToAction("Create");
      
        }
        public IActionResult Index()
        {
            var donors = _db.Donors.ToList();
            return View(donors);
        }
        }

}
