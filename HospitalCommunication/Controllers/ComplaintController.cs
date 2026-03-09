using HospitalCommunication.Data;
using HospitalCommunication.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalCommunication.Controllers
{
    public class ComplaintController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComplaintController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Sender page - anonymous, no login needed
        public IActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                complaint.SubmittedAt = DateTime.Now;
                complaint.IsReviewed = false;
                _context.Complaints.Add(complaint);
                await _context.SaveChangesAsync();
                return RedirectToAction("ThankYou");
            }
            return View(complaint);
        }

        public IActionResult ThankYou()
        {
            return View();
        }
    }
}