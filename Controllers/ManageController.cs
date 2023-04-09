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

        public IActionResult Index()
        {
            return View();
        }
    }
}
