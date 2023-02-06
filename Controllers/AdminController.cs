using System.Security.Claims;
using ChatThreeRole.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ChatThreeRole.Controllers;
public class AdminController: Controller{
    
    public IActionResult Login(){
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(AccountModel accountModel){
        if (accountModel.Email == "admin" && accountModel.Password == "admin")
        {
            var claimsAd = new Claim[]
            {
                new Claim(ClaimTypes.Email, "admin@example.com"),
                new Claim(ClaimTypes.NameIdentifier, "admin"),
                new Claim("Description", "Description content")
            };
            var claimsIdentity = new ClaimsIdentity[] { new ClaimsIdentity(claimsAd, "CookieAdmin") };
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync("CookieAdmin",claimsPrincipal);
            return RedirectToAction("index", "admin");
        }
        return View();
    }
    [Authorize(AuthenticationSchemes = "CookieAdmin")]
    public IActionResult Index(){
        return View();
    }
}