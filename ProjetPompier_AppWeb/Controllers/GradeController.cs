using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetPompier_AppWeb.Logics.Models;
using ProjetPompier_AppWeb.Utils;
using System.Json;

namespace ProjetPompier_AppWeb.Controllers
{
    public class GradeController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// </summary>
        /// <returns>Retourne l'index</returns>
        [Route("Grade")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Grade/ObtenirListeGrade");
            List<GradeDTO> listeGradeDTO = JsonConvert.DeserializeObject<List<GradeDTO>>(jsonResponse.ToString());
            ViewBag.ListeGrade = listeGradeDTO;
            return View();
        }

        /// <summary>
        /// Méthode de service appelé lors de l'action AjouterGrade.
        /// </summary>
        /// <param name="grade">Le gardeDTO</param>
        /// <returns></returns>
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

        /// <summary>
        /// Méthode de service appelé lors de l'action FormulaireModifierGrade.
        /// </summary>
        /// <param name="description">La description du grade</param>
        /// <returns></returns>
        [Route("/Grade/FormulaireModifierGrade")]
        [HttpGet]
        public async Task<IActionResult> FormulaireModifierGrade(string description)
        {
            JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Grade/ObtenirGrade?description=" + description);
            GradeDTO gradeDTO = JsonConvert.DeserializeObject<GradeDTO>(jsonResponse.ToString());
            ViewBag.descriptionAvantChangement = description;

            return View();
        }


        /// <summary>
        /// Méthode de service appelé lors de l'action ModifierGrade.
        /// </summary>
        /// <param name="descriptionAvantChangement">Description avant changement</param>
        /// <param name="grade">Le nouveau gradeDTO</param>
        /// <returns></returns>
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

        /// <summary>
        /// Méthode de service appelé lors de l'action SupprimerGrade.
        /// </summary>
        /// <param name="description">Description du grade</param>
        /// <returns></returns>
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

        /// <summary>
        /// Méthode de service appelé lors de l'action ViderListeGrade.
        /// </summary>
        /// <returns></returns>
        [Route("/Grade/ViderListeGrade")]
        [HttpPost]
        public async Task<IActionResult> ViderListeGrade()
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Grade/ViderListeGrade", null);
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
