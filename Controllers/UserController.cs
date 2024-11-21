using FitTrack.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitTrack.Controllers
{
    //[Authorize(Roles = "User")]
    public class UserController : Controller
    {
        private readonly FitTrackContext _context;

        public UserController(FitTrackContext context)
        {
            _context = context;
        }
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
