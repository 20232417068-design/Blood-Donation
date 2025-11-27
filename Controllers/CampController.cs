using BloodDonation.Data;
using BloodDonation.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.Controllers
{
    public class CampController : Controller
    {
        private readonly AppDbContext _db;

        public CampController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var camps = _db.Camps.OrderBy(c => c.Date).ToList();
            ViewBag.IsAdmin = HttpContext.Session.GetString("AdminLoggedIn") == "true";
            return View(camps);
        }

        // ADD CAMP
        public IActionResult Create()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Admin");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Camp camp)
        {
            camp.Description ??= ""; // avoid null error

            _db.Camps.Add(camp);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // EDIT CAMP
        public IActionResult Edit(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Admin");

            var camp = _db.Camps.Find(id);
            if (camp == null) return NotFound();

            return View(camp);
        }

        [HttpPost]
        public IActionResult Edit(Camp camp)
        {
            camp.Description ??= "";

            _db.Camps.Update(camp);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // DELETE CAMP
        public IActionResult Delete(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Admin");

            var camp = _db.Camps.Find(id);
            if (camp == null) return NotFound();

            _db.Camps.Remove(camp);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("AdminLoggedIn") == "true";
        }
    }
}
