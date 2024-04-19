using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetPompier_AppWeb.Logics.Models;
using ProjetPompier_AppWeb.Utils;
using System.Json;

namespace ProjetPompier_AppWeb.Controllers
{
    public class PompierController : Controller
    {
        [Route("Pompier")]
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string nomCaserne, [FromQuery] string seulementCapitaine)
        {
            JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Caserne/ObtenirListeCaserne");
            List<CaserneDTO> listeCaserneDTO = JsonConvert.DeserializeObject<List<CaserneDTO>>(jsonResponse.ToString());
            ViewBag.ListeCaserne = listeCaserneDTO;

            if (string.IsNullOrEmpty(nomCaserne) && string.IsNullOrEmpty(seulementCapitaine))
            {
                nomCaserne = listeCaserneDTO.FirstOrDefault()?.Nom; // Utiliser le premier nom de caserne si aucun n'est spécifié
                seulementCapitaine = "false"; // Utiliser la valeur par défaut si rien n'est spécifié
            }

            jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Pompier/ObtenirListePompier?nomCaserne=" + nomCaserne + "&seulementCapitaine=" + seulementCapitaine);
            List<PompierDTO> listePompierDTO = JsonConvert.DeserializeObject<List<PompierDTO>>(jsonResponse.ToString());
            ViewBag.ListePompier = listePompierDTO;
            ViewBag.NomCaserne = nomCaserne;
            ViewBag.SeulementCapitaine = seulementCapitaine;

            return View(); // Retourner la vue
        }

    }
}
