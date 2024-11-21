using FitTrack.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitTrack.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly FitTrackContext _context;

        public AdminController(FitTrackContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}