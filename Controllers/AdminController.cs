using BloodDonation.Data;
using BloodDonation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BloodDonation.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _db;

        public AdminController(AppDbContext db)
        {
            _db = db;
        }

        // ===================== LOGIN =========================

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Username and Password are required";
                return View();
            }

            var admin = _db.Admins.FirstOrDefault(a =>
                a.Username == username && a.Password == password);

            if (admin != null)
            {
                HttpContext.Session.SetString("AdminLoggedIn", "true");
                HttpContext.Session.SetString("AdminName", admin.Username);
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid Login";
            return View();
        }

        // ================== DASHBOARD WITH FILTER ===============

        public IActionResult Dashboard(string bloodGroup, string city)
        {
            if (!IsAdminLoggedIn()) return RedirectToAction("Login");

            var donors = _db.Donors.AsQueryable();

            if (!string.IsNullOrWhiteSpace(bloodGroup))
                donors = donors.Where(d => d.BloodGroup == bloodGroup);

            if (!string.IsNullOrWhiteSpace(city))
                donors = donors.Where(d => d.City.Contains(city));

            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.SelectedBloodGroup = bloodGroup;
            ViewBag.SelectedCity = city;

            return View(donors.ToList());
        }

        // ====================== EDIT ==========================

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsAdminLoggedIn()) return RedirectToAction("Login");

            var donor = _db.Donors.FirstOrDefault(d => d.Id == id);
            if (donor == null) return NotFound();

            return View(donor);
        }

        [HttpPost]
        public IActionResult Edit(Donor d)
        {
            if (!IsAdminLoggedIn()) return RedirectToAction("Login");

            _db.Donors.Update(d);
            _db.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        // ===================== DELETE =========================

        public IActionResult Delete(int id)
        {
            if (!IsAdminLoggedIn()) return RedirectToAction("Login");

            var donor = _db.Donors.FirstOrDefault(d => d.Id == id);
            if (donor == null) return NotFound();

            _db.Donors.Remove(donor);
            _db.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        // ====================== LOGOUT ========================

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // ================= SESSION CHECK ======================

        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.GetString("AdminLoggedIn") == "true";
        }
    }
}
