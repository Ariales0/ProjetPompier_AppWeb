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

        [Route("/Grade/AjouterGrade")]
        [HttpPost]
        public async Task<IActionResult> AjouterGrade(GradeDTO grade)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Grade/AjouterGrade", grade);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
                TempData["MessageErreur"] = e.Message;
            }

            return RedirectToAction("Index");
        }

        [Route("/Grade/FormulaireModifierGrade")]
        [HttpGet]
        public async Task<IActionResult> FormulaireModifierGrade(string description)
        {
            JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Grade/ObtenirGrade?description=" + description);
            GradeDTO gradeDTO = JsonConvert.DeserializeObject<GradeDTO>(jsonResponse.ToString());
            ViewBag.descriptionAvantChangement = description;

            return View();
        }



        [Route("/Grade/ModifierGrade")]
        [HttpPost]
        public async Task<IActionResult> ModifierGrade(string descriptionAvantChangement, GradeDTO grade)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Grade/ModifierGrade?descriptionAvantChangement=" + descriptionAvantChangement + "&descriptionApresChangement=" + grade.Description, null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
                TempData["MessageErreur"] = e.Message;
            }

            return RedirectToAction("Index");
        }

        [Route("/Grade/SupprimerGrade")]
        [HttpPost]
        public async Task<IActionResult> SupprimerGrade(string description)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Grade/SupprimerGrade?description=" + description, null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
                TempData["MessageErreur"] = e.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
