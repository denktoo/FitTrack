using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FitTrack.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}