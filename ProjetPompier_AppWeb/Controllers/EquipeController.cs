using ProjetPompier_AppWeb.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetPompier_AppWeb.Logics.Models;
using System.Json;
using ProjetPompier_AppWeb.Logics.Models;
using static System.Net.Mime.MediaTypeNames;

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
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string nomCaserne, [FromQuery] int matriculeCapitaine, [FromQuery] string dateIntervention)
        {
            ViewData["Title"] = "Équipe(s)";
            try
            {
                ViewBag.NomCaserne = nomCaserne;
                ViewBag.MatriculeCapitaine = matriculeCapitaine;
                ViewBag.DateIntervention = dateIntervention;

                

                
                

                

                JsonValue jsonValue = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Vehicule/ObtenirListeVehicule?nomCaserne=" + nomCaserne + "&disponibleSeulement=" + true);
                List<VehiculeDTO> listeVehiculeDTO = JsonConvert.DeserializeObject<List<VehiculeDTO>>(jsonValue.ToString());
                ViewBag.ListeVehicule = listeVehiculeDTO;

                jsonValue = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Vehicule/ObtenirListeVehicule?nomCaserne=" + nomCaserne + "&disponibleSeulement=" + false);
                List<VehiculeDTO> listeToutVehiculeDTO = JsonConvert.DeserializeObject<List<VehiculeDTO>>(jsonValue.ToString());
                ViewBag.ListeToutVehicule = listeToutVehiculeDTO;

                jsonValue = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Pompier/ObtenirListePompierDisponible?nomCaserne=" + nomCaserne);
                List<PompierDTO> listePompierDTO = JsonConvert.DeserializeObject<List<PompierDTO>>(jsonValue.ToString());
                ViewBag.ListePompier = listePompierDTO;

                jsonValue = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/TypesVehicule/ObtenirListeTypesVehicule");
                List<TypeVehiculeDTO> listeTypeVehiculeDTO = JsonConvert.DeserializeObject<List<TypeVehiculeDTO>>(jsonValue.ToString());
                ViewBag.listeTypeVehicule= listeTypeVehiculeDTO;
                if(ViewBag.VehiculeSelectionne == null)
                {
                    ViewBag.VehiculeSelectionne = listeVehiculeDTO.FirstOrDefault();
                }

                jsonValue = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Equipe/ObtenirListeEquipe?nomCaserne=" + nomCaserne + "&matriculeCapitaine=" + matriculeCapitaine + "&dateDebutIntervention=" + dateIntervention);
                List<EquipeDTO> listeEquipeDTO = JsonConvert.DeserializeObject<List<EquipeDTO>>(jsonValue.ToString());
                ViewBag.ListeEquipe = listeEquipeDTO;


                

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
        public async Task<IActionResult> AjouterPompierEquipe([FromQuery] string nomCaserne, [FromQuery] int matriculePompierEquipe, [FromQuery] string vinVehicule, [FromQuery] int matriculeCapitaine, [FromQuery] string dateIntervention)
        {

            
            await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Equipe/AjouterPompierEquipe?nomCaserne=" + nomCaserne + "&matriculeCapitaine=" + matriculeCapitaine + "&dateDebutIntervention=" + dateIntervention + "&vinVehicule=" + vinVehicule + "&matriculePompier=" + matriculePompierEquipe, null );
            return RedirectToAction("Index", "Equipe", new { nomCaserne, matriculeCapitaine, dateIntervention});
        }

        [Route("/Equipe/IsEquipePleine")]
        [HttpPost]
        public bool IsEquipePleine([FromForm]int nombreMax, [FromForm]List<PompierDTO> unListeDePompiers)
        {
            if(unListeDePompiers.Count >= nombreMax)
            {
                ViewBag.IsEquipePleine = true;
                return true;
            }
            else
            {
                ViewBag.IsEquipePleine = false;
                return false;
            }
        }
    }
}
