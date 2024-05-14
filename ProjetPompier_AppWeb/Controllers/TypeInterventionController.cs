using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetPompier_AppWeb.Logics.Models;
using ProjetPompier_AppWeb.Utils;
using System.ComponentModel;
using System.Json;

namespace ProjetPompier_AppWeb.Controllers
{
    public class TypeInterventionController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///   Afficher et exécuter le fichier index.cshtml du répertoire Views/TypeVehicule
        /// </summary>
        /// <returns>IActionResult</returns>
        [Route("TypeIntervention")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/TypesIntervention/ObtenirListeTypesIntervention");
            List<TypeInterventionDTO> listeTypeIntervention= JsonConvert.DeserializeObject<List<TypeInterventionDTO>>(jsonResponse.ToString());
            //ViewBag.Liste prend la valeur de la liste des casernes
            ViewBag.ListeTypeIntervention = listeTypeIntervention;

            return View();
        }

        /// <summary>
		/// Méthode de service appelé lors de l'action AjouterTypeIntervention.
		/// Rôles de l'action : 
		///   -Ajouter un type d'intervention.
		/// </summary>
		/// <param name="typeVehiculeDTO">Le DTO d'un type d'intervention.</param>
		/// <returns>IActionResult</returns>
        [Route("/TypeIntervention/AjouterTypeIntervention")]
        [HttpPost]
        public async Task<IActionResult> AjouterTypeIntervention([FromForm]TypeInterventionDTO typeInterventionDTO)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/TypesIntervention/AjouterTypeIntervention", typeInterventionDTO);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreurCritique = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }

        /// <summary>
		/// Action FormulaireModifierTypeIntervention.
		/// Permet d'afficher le formulaire pour la modification d'un type d'intervention.
		/// </summary> 
		/// <param name="codeTypeVehicule">Le code unique du type d'intervention</param>
		/// <returns>IActionResult</returns>
        [Route("/TypeIntervention/FormulaireModifierTypeIntervention")]
        [HttpGet]
        public async Task<IActionResult> FormulaireModifierTypeIntervention([FromQuery] int codeTypeIntervention)
        {
            try
            {
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/TypesIntervention/ObtenirTypeIntervention?code=" + codeTypeIntervention);
                TypeInterventionDTO typeInterventionDTO = JsonConvert.DeserializeObject<TypeInterventionDTO>(jsonResponse.ToString());
                return View(typeInterventionDTO);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreurCritique = e.Message;
                return RedirectToAction("Index");
            }
        }

        /// <summary>
		/// Action ModifierTypeIntervention.
		/// Permet de modifier un type d'intervention.
		/// </summary>
		/// <param name="typeVehiculeDTO">Le DTO du type d'intervention </param>
		/// <returns>ActionResult</returns>
        [Route("/TypeIntervention/ModifierTypeIntervention")]
        [HttpPost]
        public async Task<IActionResult> ModifierTypeIntervention([FromForm] TypeInterventionDTO typeInterventionDTO)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/TypesIntervention/ModifierTypeIntervention", typeInterventionDTO);

            }
            catch (Exception e)
            {
                ViewBag.MessageErreurCritique = e.Message;
                return RedirectToAction("FormulaireModifierTypeIntervention", "TypeIntervention", new { codeTypeIntervention = typeInterventionDTO.Code });
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }

        /// <summary>SupprimerTypeIntervention.
		/// Permet de supprimer un  type d'intervention.
		/// </summary>
		/// <param name="codeTypeVehicule">Le code unique du type d'intervention</param>
		/// <returns>ActionResult</returns>
        [Route("/TypeIntervention/SupprimerTypeIntervention")]
        [HttpPost]
        public async Task<IActionResult> SupprimerTypeIntervention([FromForm]int codeTypeIntervention)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/TypesIntervention/SupprimerTypeIntervention?code=" + codeTypeIntervention, null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreurCritique = e.Message;
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }

        /// <summary>
		/// Action ViderListeTypeVehicule.
		/// Permet de supprimer tous les types d'interventions.
		/// </summary>
		/// <returns>ActionResult</returns>
        [Route("/TypeIntervention/ViderListeTypeIntervention")]
        [HttpPost]
        public async Task<IActionResult> ViderListeTypeIntervention()
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/TypesIntervention/ViderListeTypesIntervention", null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreurCritique = e.Message;
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }

        [HttpGet]
        public  async void ObtenirListeTypesIntervention()
        {
            List<TypeInterventionDTO> listeTypeInterventionDansBDD;

            JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/TypesIntervention/ObtenirListeTypesIntervention");
            List<TypeInterventionDTO> listeTypeIntervention = JsonConvert.DeserializeObject<List<TypeInterventionDTO>>(jsonResponse.ToString());

             ViewData["skibidiRizz"] = listeTypeIntervention;



        }
    }

}
