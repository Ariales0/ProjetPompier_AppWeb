using ProjetPompier_AppWeb.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetPompier_AppWeb.Logics.Models;
using System.Json;
using ProjetPompier_AppWeb.Logics.Models;

namespace ProjetPompier_AppWeb.Controllers
{
    public class EquipeController : Controller
    {

        /// <summary>
		/// Méthode de service appelé lors de l'action Index.
		/// Rôles de l'action : 
		///   Afficher et exécuter le fichier index.cshtml du répertoire Views/Intervention
		/// </summary>
		/// <returns>IActionResult</returns>
        [Route("Equipe")]
        [HttpPost]
        public async Task<IActionResult> Index([FromQuery] string nomCaserne, [FromQuery] int matriculeCapitaine, [FromQuery] string dateIntervention, [FromBody] List<PompierDTO> listePompierEquipe)
        {
            ViewData["Title"] = "Équipe(s)";
            try
            {
                ViewBag.NomCaserne = nomCaserne;
                ViewBag.MatriculeCapitaine = matriculeCapitaine;
                ViewBag.DateIntervention = dateIntervention;
                ViewBag.ListePompierEquipe = listePompierEquipe;

                ViewBag.CountPompier = 0;
                

                
                

                

                JsonValue jsonValue = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Vehicule/ObtenirListeVehicule?nomCaserne=" + nomCaserne + "&disponibleSeulement=" + true);
                List<VehiculeDTO> listeVehiculeDTO = JsonConvert.DeserializeObject<List<VehiculeDTO>>(jsonValue.ToString());
                ViewBag.ListeVehicule = listeVehiculeDTO;

                jsonValue = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Pompier/ObtenirListePompierDisponible?nomCaserne=" + nomCaserne);
                List<PompierDTO> listePompierDTO = JsonConvert.DeserializeObject<List<PompierDTO>>(jsonValue.ToString());
                ViewBag.ListePompier = listePompierDTO;

                jsonValue = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/TypesVehicule/ObtenirListeTypesVehicule");
                List<TypeVehiculeDTO> listeTypeVehiculeDTO = JsonConvert.DeserializeObject<List<TypeVehiculeDTO>>(jsonValue.ToString());
                ViewBag.listeTypeVehicule= listeTypeVehiculeDTO;



                return View();
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
		/// Méthode de service appelé lors de l'action OuvrirFicheIntervention.
		/// Rôles de l'action : 
		///   -Ouvrir une fiche d'intervention.
		/// </summary>
		/// <param name="nomCaserne">Nom de la caserne.</param>
        /// /// <param name="ficheInterventionDTO">Le DTO d'une intervention.</param>
		/// <returns>IActionResult</returns>
        [Route("/Equipe/AjouterEquipe")]
        [HttpPost]
        public async Task<IActionResult> AjouterEquipe([FromForm] string nomCaserne, [FromForm] int matriculeCapitaine, [FromForm] string dateFicheIntervention, EquipeDTO equipe )
        {
           return RedirectToAction("Index", "Intervention", new { nomCaserne, matriculeCapitaine, dateIntervention = dateFicheIntervention });
        }

       
        /// <summary>
		/// Action FermerFicheIntervention.
		/// Permet de fermer une fiche d'intervention.
		/// </summary> 
		/// <param name="nomCaserne">Nom de la caserne.</param>
		/// <param name="matriculeCapitaine">Matricule du pompier capitaine en charge de l'intervention.</param>
		/// <param name="dateDebut">Date du debut de l'intervention.</param>
		/// <returns>async Task<IActionResult></returns>
		[Route("/Equipe/SupprimerEquipe")]
        [HttpPost]
        public async Task<IActionResult> SupprimerEquipe([FromForm] string nomCaserne, [FromForm] int matriculeCapitaine, [FromForm] string dateDebut, [FromForm] int codeEquipe)
        {
           
            return RedirectToAction("Index", "Intervention", new { nomCaserne, matriculeCapitaine });
        }

        [Route("/Equipe/AjouterPompierEquipe")]
        [HttpGet]
        public async Task<IActionResult> AjouterPompierEquipe([FromQuery] string nomCaserne, [FromQuery] int matriculePompierEquipe, [FromQuery] int matriculeCapitaine, [FromQuery] string dateIntervention) 
        {
            JsonValue jsonValue = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Pompier/ObtenirPompier?nomCaserne=" + nomCaserne + "&matriculePompier=" + matriculePompierEquipe);
            PompierDTO pompierDTO = JsonConvert.DeserializeObject<PompierDTO>(jsonValue.ToString());

            List<PompierDTO> listePompierEquipe = new List<PompierDTO>();
            listePompierEquipe.Add(pompierDTO);

            ViewBag.ListePompierEquipe = listePompierEquipe;
            TempData["ListePompierEquipe"] = listePompierEquipe.ToString();
            return RedirectToAction("Index", "Equipe", new { nomCaserne, matriculeCapitaine, dateIntervention, listePompierEquipe});
        }
    }
}
