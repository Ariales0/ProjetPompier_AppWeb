using ProjetPompier_AppWeb.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetPompier_AppWeb.Logics.Models;
using System.Json;

namespace ProjetPompier_AppWeb.Controllers
{
    public class CaserneController : Controller
    {

        /// <summary>
		/// Méthode de service appelé lors de l'action Index.
		/// Rôles de l'action : 
		///   Afficher et exécuter le fichier index.cshtml du répertoire Views/Caserne
		/// </summary>
		/// <returns>IActionResult</returns>
        [Route("")]
        [Route("Caserne")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //ViewBag.NbCaserne prend la valeur du nombre de casernes dans la liste
            JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Caserne/ObtenirListeCaserne");
            List<CaserneDTO> listeCaserneDTO = JsonConvert.DeserializeObject<List<CaserneDTO>>(jsonResponse.ToString());
            ViewBag.NbCaserne = listeCaserneDTO.Count;
            //ViewBag.Liste prend la valeur de la liste des casernes
            ViewBag.ListeCaserne = listeCaserneDTO;
            return View();
        }

        /// <summary>
		/// Méthode de service appelé lors de l'action AjouterCaserne.
		/// Rôles de l'action : 
		///   -Ajouter une caserne.
		/// </summary>
		/// <param name="caserneDTO">Le DTO d'une caserne.</param>
		/// <returns>IActionResult</returns>
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

        /// <summary>
		/// Action FormulaireModifierCaserne.
		/// Permet d'afficher le formulaire pour la modification d'une caserne.
		/// </summary> 
		/// <param name="nomCaserne">Nom de la caserne à modifier.</param>
		/// <returns>IActionResult</returns>
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

        /// <summary>
		/// Action ModifierCaserne.
		/// Permet de modifier une caserne.
		/// </summary>
		/// <param name="caserneDTO">Le DTO de la caserne à modifier.</param>
		/// <returns>ActionResult</returns>
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

        /// <summary>aserne.
		/// Permet de supprimer une caserne.
		/// </summary>
		/// <param name="nomCaserne">Le nom de la caserne.</param>
		/// <returns>ActionResult</returns>
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

        /// <summary>
		/// Action SupprimerCaserne.
		/// Permet de supprimer une caserne.
		/// </summary>
		/// <returns>ActionResult</returns>
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
