using Microsoft.AspNetCore.Mvc;
using ProjetPompier_API.Logics.Models;

namespace ProjetPompier_AppWeb.Controllers
{
    public class CaserneController : Controller
    {
        [Route("")]
        [Route("Caserne")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Route("Caserne/AjouterCaserne")]
        [HttpGet]
        public async Task<IActionResult> AjouterCaserne(CaserneDTO caserneDTO)
        {
            return View();
        }

        [Route("Caserne/FormulaireModificationCaserne")]
        [HttpGet]
        public async Task<IActionResult> FormulaireModificationCaserne(string nomCaserne)
        {
            return View();
        }

        [Route("Caserne/ModifierCaserne")]
        [HttpGet]
        public async Task<IActionResult> ModifierCaserne(CaserneDTO caserneDTO)
        {
            return View();
        }

        [Route("Caserne/SupprimerCaserne")]
        [HttpGet]
        public async Task<IActionResult> SupprimerCaserne(string nomCaserne)
        {
            return View();
        }

        [Route("Caserne/ViderListeCaserne")]
        [HttpGet]
        public async Task<IActionResult> ViderListeCaserne()
        {
            return View();
        }
    }
}
