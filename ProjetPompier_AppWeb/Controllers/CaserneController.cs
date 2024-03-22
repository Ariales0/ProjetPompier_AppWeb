using Microsoft.AspNetCore.Mvc;

namespace ProjetPompier_AppWeb.Controllers
{
    public class CaserneController : Controller
    {
        [Route("")]
        [Route("Caserne")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
