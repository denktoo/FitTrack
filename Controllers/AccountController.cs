using Microsoft.AspNetCore.Mvc;
using FitTrack.ViewModels;
using FitTrack.Models;
using FitTrack.Data;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication;

namespace FitTrack.Controllers
{
    public class AccountController : Controller
    {
        private readonly FitTrackContext _context;
        private readonly IConfiguration _configuration;

        public AccountController(FitTrackContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Check if email is already in use
            if (_context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email is already in use.");
                return View(model);
            }

            // Create user and set default role to "User"
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Set role using shadow property
            _context.Entry(user).Property("Role").CurrentValue = "User";
            await _context.SaveChangesAsync();

            // Redirect to login after registration
            return RedirectToAction("Login");
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Authenticate user
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            // Create JWT token with claims
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var role = user.Username == "admin" ? "Admin" : "User";

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Store token in cookie or local storage for subsequent requests
            HttpContext.Response.Cookies.Append("AuthToken", tokenString, new CookieOptions { HttpOnly = true });

            // Redirect based on role
            return role == "Admin" ? RedirectToAction("Dashboard", "Admin") : RedirectToAction("Dashboard", "User");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync();

            // Redirect to home or login page after logout
            return RedirectToAction("Index", "Home");
        }
    }
}
