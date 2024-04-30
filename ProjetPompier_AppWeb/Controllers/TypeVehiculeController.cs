using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetPompier_AppWeb.Logics.Models;
using ProjetPompier_AppWeb.Utils;
using System.Json;

namespace ProjetPompier_AppWeb.Controllers
{
    public class TypeVehiculeController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///   Afficher et exécuter le fichier index.cshtml du répertoire Views/TypeVehicule
        /// </summary>
        /// <returns>IActionResult</returns>
        [Route("TypeVehicule")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/TypesVehicule/ObtenirListeTypesVehicule");
            List<TypeVehiculeDTO> listeTypeVehicule= JsonConvert.DeserializeObject<List<TypeVehiculeDTO>>(jsonResponse.ToString());
            //ViewBag.Liste prend la valeur de la liste des casernes
            ViewBag.ListeTypeVehicule = listeTypeVehicule;
            return View();
        }

        /// <summary>
		/// Méthode de service appelé lors de l'action AjouterTypeVehicule.
		/// Rôles de l'action : 
		///   -Ajouter un type de véhicule.
		/// </summary>
		/// <param name="typeVehiculeDTO">Le DTO d'un type de vehicule.</param>
		/// <returns>IActionResult</returns>
        [Route("/TypeVehicule/AjouterTypeVehicule")]
        [HttpPost]
        public async Task<IActionResult> AjouterTypeVehicule([FromForm]TypeVehiculeDTO typeVehiculeDTO)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/TypesVehicule/AjouterTypesVehicule", typeVehiculeDTO);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreurCritique = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }

        /// <summary>
		/// Action FormulaireModifierTypeVehicule.
		/// Permet d'afficher le formulaire pour la modification d'un type de véhicule.
		/// </summary> 
		/// <param name="codeTypeVehicule">Le code unique du type de véhicule</param>
		/// <returns>IActionResult</returns>
        [Route("/TypeVehicule/FormulaireModifierTypeVehicule")]
        [HttpGet]
        public async Task<IActionResult> FormulaireModifierTypeVehicule([FromQuery] int codeTypeVehicule)
        {
            try
            {
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Typesvehicule/ObtenirTypesVehicule?code=" + codeTypeVehicule);
                TypeVehiculeDTO typeVehiculeDTO = JsonConvert.DeserializeObject<TypeVehiculeDTO>(jsonResponse.ToString());
                return View(typeVehiculeDTO);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreurCritique = e.Message;
                return RedirectToAction("Index");
            }
        }

        /// <summary>
		/// Action ModifierTypeVehicule.
		/// Permet de modifier un type de véhicule.
		/// </summary>
		/// <param name="typeVehiculeDTO">Le DTO du type de véhicule</param>
		/// <returns>ActionResult</returns>
        [Route("/TypeVehicule/ModifierTypeVehicule")]
        [HttpPost]
        public async Task<IActionResult> ModifierTypeVehicule([FromForm] TypeVehiculeDTO typeVehiculeDTO)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/TypesVehicule/ModifierTypesVehicule", typeVehiculeDTO);

            }
            catch (Exception e)
            {
                ViewBag.MessageErreurCritique = e.Message;
                return RedirectToAction("FormulaireModifierTypeVehicule", "TypeVehicule", new { codeTypeVehicule = typeVehiculeDTO.Code });
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }

        /// <summary>SupprimerTypeVehicule.
		/// Permet de supprimer un  type de véhicule.
		/// </summary>
		/// <param name="codeTypeVehicule">Le code unique du type de véhicule</param>
		/// <returns>ActionResult</returns>
        [Route("/TypeVehicule/SupprimerTypeVehicule")]
        [HttpPost]
        public async Task<IActionResult> SupprimerTypeVehicule([FromForm]int codeTypeVehicule)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/TypesVehicule/SupprimerTypesVehicule?code=" + codeTypeVehicule, null);
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
        [Route("/TypeVehicule/ViderListeTypeVehicule")]
        [HttpPost]
        public async Task<IActionResult> ViderListeTypeVehicule()
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/TypesVehicule/ViderListeTypesVehicules", null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreurCritique = e.Message;
            }
            //Lancement de l'action Index...
            return RedirectToAction("Index");
        }
    }
}
