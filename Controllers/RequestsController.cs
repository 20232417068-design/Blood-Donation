using BloodDonation.Data;
using BloodDonation.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.Controllers
{
    public class RequestsController : Controller
    {
        private readonly AppDbContext _db;

        public RequestsController(AppDbContext db)
        {
            _db = db;
        }

        // SHOW FORM
        public IActionResult Create()
        {
            return View();
        }

        // SUBMIT FORM
        [HttpPost]
        public IActionResult Create(Request model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.RequestDate = DateTime.Now;
            _db.Requests.Add(model);
            _db.SaveChanges();

            TempData["Success"] = "Your blood request has been submitted!";

            ModelState.Clear(); // CLEAR FORM
            return RedirectToAction("Create");
        }

        // LIST REQUESTS
        public IActionResult Index()
        {
            var data = _db.Requests.OrderByDescending(r => r.Id).ToList();
            return View(data);
        }

        // DELETE REQUEST
        public IActionResult Delete(int id)
        {
            var req = _db.Requests.FirstOrDefault(r => r.Id == id);

            if (req != null)
            {
                _db.Requests.Remove(req);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
