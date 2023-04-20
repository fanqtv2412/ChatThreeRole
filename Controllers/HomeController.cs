using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChatThreeRole.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;

namespace ChatThreeRole.Controllers;

public class HomeController : Controller
{
    public static ConcurrentDictionary<string, int> listAccount = new ConcurrentDictionary<string, int>();
    private readonly ILogger<HomeController> _logger;
    private readonly MyDBContext _context;
    public HomeController(ILogger<HomeController> logger, MyDBContext context)
    {
        _context = context;
        _logger = logger;
    }
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
        var user = _context.Account.Find(accountModel.Email);
        if (user == null)
        {
            ViewBag.Notify = "Can not find this email";
            return View(accountModel);
        }
        else
        {
            if (user.Password == accountModel.Password)
            {
                var claims = new List<Claim>(){
                    new Claim(ClaimTypes.NameIdentifier, user.Email),
                    new Claim("Description", "Description Content")
                };
                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principle = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("CookieAuth", principle);
                listAccount.AddOrUpdate(user.Email, 1, (key, oldValue) => oldValue + 1);
                return (RedirectToAction("Index", new { name = user.FullName, email = user.Email }));
            }
            else
            {
                ViewBag.Notify = "incorrect password";
                return View(accountModel);
            }
        }
    }

    [HttpGet]
    public async Task<IActionResult> Logout(string email)
    {
        //SignOutAsync is Extension method for SignOut    
        await HttpContext.SignOutAsync("CookieAuth");
        if(email!= null){
            int item = listAccount.GetValueOrDefault(email);
            if(item == 0) return RedirectToAction("Login");
            if(item == 1){
                listAccount.Remove(email, out int value);
            }
            if(item >1){
                if(listAccount.TryUpdate(email, item-1, item)){
                    return RedirectToAction("Login");
                }
            }
        }
        //Redirect to home page    
        return RedirectToAction("Login");
    }

    public ActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async  Task<ActionResult> NewGroup(GroupModel group){
        if(ModelState.IsValid){
            _context.Group.Add(group);
            ViewBag.Notify = "Add Group succesful";
            await _context.SaveChangesAsync();
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Register(AccountModel account)
    {
        if (ModelState.IsValid)
        {
            var userAccount = _context.Account.Find(account.Email);
            if (userAccount == null)
            {
                _context.Account.Add(account);
                _context.SaveChanges();
                ViewBag.Notify = "Register successfull";
                return View("Login");
            }
            else
            {
                ViewBag.Notify = "Email used!!! Chosse another Email!";
                return View();
            }
        }
        ViewBag.Notify = "Somthing is wrong, try again";
        return View();
    }

    [Authorize(AuthenticationSchemes = "CookieAuth")]
    public IActionResult Index(string name, string email)
    {
        ViewBag.TotalAccount = listAccount.Count;
        ViewBag.Name = name;
        ViewBag.Email = email;
        var user = _context.Account.Find(email);
        if(user != null )
            ViewBag.Avatar = user.Avatar;
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

    [HttpGet]
    public IActionResult ChatGroup(string email, string name){
        return View();
    }
}
