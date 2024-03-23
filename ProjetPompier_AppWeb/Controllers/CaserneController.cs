using ProjetPompier_API.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetPompier_API.Logics.Models;
using System.Json;

namespace ProjetPompier_AppWeb.Controllers
{
    public class CaserneController : Controller
    {
        [Route("")]
        [Route("Caserne")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //ViewBag.NbCaserne prend la valeur du nombre de casernes dans la liste
            JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Caserne/ObtenirListeCaserne");
            List<CaserneDTO> listeCaserneDTO = JsonConvert.DeserializeObject<List<CaserneDTO>>(jsonResponse.ToString());
            ViewBag.NbCaserne = listeCaserneDTO.Count;
            //ViewBag.Liste prend la valeur de la liste des cégeps
            ViewBag.ListeCaserne = listeCaserneDTO;
            return View();
        }

        [Route("/Caserne/AjouterCaserne")]
        [HttpPost]
        public async Task<IActionResult> AjouterCaserne(CaserneDTO caserneDTO)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Caserne/AjouterCaserne", caserneDTO);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }

        [Route("/Caserne/FormulaireModifierCaserne")]
        [HttpGet]
        public async Task<IActionResult> FormulaireModifierCaserne([FromQuery]string nomCaserne)
        {
            try
            {
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Caserne/ObtenirCaserne?nomCaserne=" + nomCaserne);
                CaserneDTO caserneDTO = JsonConvert.DeserializeObject<CaserneDTO>(jsonResponse.ToString());
                return View(caserneDTO);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
                return RedirectToAction("Index");
            }
        }

        [Route("/Caserne/ModifierCaserne")]
        [HttpPost]
        public async Task<IActionResult> ModifierCaserne(CaserneDTO caserneDTO)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Caserne/ModifierCaserne", caserneDTO);

            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
                return RedirectToAction("FormulaireModifierCaserne", "Caserne", new { nomCaserne = caserneDTO.Nom });
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }

        [Route("/Caserne/SupprimerCaserne")]
        [HttpPost]
        public async Task<IActionResult> SupprimerCaserne(string nomCaserne)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Caserne/SupprimerCaserne?nomCaserne=" + nomCaserne, null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }

        [Route("/Caserne/ViderListeCaserne")]
        [HttpPost]
        public async Task<IActionResult> ViderListeCaserne()
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Caserne/ViderListeCaserne", null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }
    }
}
