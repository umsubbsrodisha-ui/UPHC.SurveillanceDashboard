
using Microsoft.AspNetCore.Identity;

namespace UPHC.SurveillanceDashboard.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? UPHCId { get; set; }  // nullable for Analyst/Admin
        public UPHC UPHC { get; set; }
    }
}