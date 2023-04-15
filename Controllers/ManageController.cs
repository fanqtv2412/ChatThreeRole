using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace ChatThreeRole.Controllers
{
    public class ManageController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDBContext _context;
        public ManageController(ILogger<HomeController> logger, MyDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string email)
        {
            var account = _context.Account.Find(email);
            return View(account);
        }

        [HttpPost]
        public IActionResult UpdateProfile()
        {
            string email = Request.Form["txtEmail"];
            string fullName = Request.Form["txtFullName"];
            string passWord = Request.Form["txtPassword"];
            var account = _context.Account.Find(email);
            
            if(account == null)
            {
                return RedirectToAction("Index", "Manage", new { email = email });
            }
            else
            {
                if (String.IsNullOrEmpty(passWord))
                {
                    passWord = account.Password;
                }
                try
                {
                    account.FullName = fullName;
                    account.Password = passWord;
                    var file = Request.Form.Files[0];
                    if (file != null && file.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", file.FileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            account.Avatar = file.FileName;
                        }
                    }
                    _context.Account.Update(account);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Manage", new { email = email });

                }
                catch
                {
                    return RedirectToAction("Index", "Manage", new { email = email });
                }
            }
        }
    }
}
