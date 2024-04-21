using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetPompier_AppWeb.Logics.Models;
using ProjetPompier_AppWeb.Utils;
using System.Json;

namespace ProjetPompier_AppWeb.Controllers
{
    public class GradeController : Controller
    {
        [Route("Grade")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Grade/ObtenirListeGrade");
            List<GradeDTO> listeGradeDTO = JsonConvert.DeserializeObject<List<GradeDTO>>(jsonResponse.ToString());
            ViewBag.ListeGrade = listeGradeDTO;
            return View();
        }
    }
}
