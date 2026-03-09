using HospitalCommunication.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalCommunication.Controllers
{
    [Authorize]  // Only logged-in Quality Dept users can access this
    public class QualityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QualityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Inbox - see all complaints
        public async Task<IActionResult> Inbox()
        {
            var complaints = await _context.Complaints
                .OrderByDescending(c => c.SubmittedAt)
                .ToListAsync();
            return View(complaints);
        }

        // Mark as reviewed + add internal note
        [HttpPost]
        public async Task<IActionResult> Review(int id, string notes)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint != null)
            {
                complaint.IsReviewed = true;
                complaint.Notes = notes;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Inbox");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint != null)
            {
                _context.Complaints.Remove(complaint);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Inbox");
        }
    }
}