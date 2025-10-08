using Microsoft.AspNetCore.Mvc;

namespace projetoBancoDS.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult CadCli()
        {
            return View();
        }
    }
}
