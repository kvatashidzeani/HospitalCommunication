using System.ComponentModel.DataAnnotations;

namespace HospitalCommunication.Models
{
    public class Complaint
    {
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        public string Category { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        public bool IsReviewed { get; set; } = false;

        public string? Notes { get; set; }
    }
}