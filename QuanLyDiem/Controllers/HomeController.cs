using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QuanLyDiem.Data;
using QuanLyDiem.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace QuanLyDiem.Controllers;
public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;


    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult Login()
    {
        return View();
    } 
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _context.GetUserAsync(username, password);

        if (user != null)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.username));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "SinhVien");
        }

        ModelState.AddModelError(string.Empty, "Tài khoản hoặc mật khẩu sai");
        return View();
    }
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Login", "Home");
    }

}
