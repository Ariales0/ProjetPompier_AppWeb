using ProjetPompier_AppWeb.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetPompier_AppWeb.Logics.Models;
using System.Json;

namespace ProjetPompier_AppWeb.Controllers
{
    public class InterventionController : Controller
    {

        /// <summary>
		/// Méthode de service appelé lors de l'action Index.
		/// Rôles de l'action : 
		///   Afficher et exécuter le fichier index.cshtml du répertoire Views/Intervention
		/// </summary>
		/// <returns>IActionResult</returns>
        [Route("Intervention")]
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string nomCaserne, [FromQuery] int matriculeCapitaine)
        {
            try
            {
                JsonValue jsonResponseListeCaserneDTO = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Caserne/ObtenirListeCaserne");
                List<CaserneDTO> listeCaserneDTO = JsonConvert.DeserializeObject<List<CaserneDTO>>(jsonResponseListeCaserneDTO.ToString());

                if (nomCaserne == null || matriculeCapitaine == 0)
                {
                    nomCaserne = listeCaserneDTO.FirstOrDefault()?.Nom;
                    if (string.IsNullOrEmpty(nomCaserne))
                    {
                        ViewBag.MessageErreurCritique = "Aucune caserne trouvée.";
                        return View();
                    }

                    JsonValue jsonResponseListeCapitaineDTO = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Pompier/ObtenirListeCapitaine?nomCaserne=" + nomCaserne + "&seulementCapitaine=true");
                    List<PompierDTO> listeCapitaineDTO = JsonConvert.DeserializeObject<List<PompierDTO>>(jsonResponseListeCapitaineDTO.ToString());
                    if (listeCapitaineDTO.Count == 0)
                    {
                        ViewBag.MessageErreurCritique = "Aucun capitaine dans cette caserne";
                        return View();
                    }

                    matriculeCapitaine = listeCapitaineDTO[0].Matricule;
                }

                ViewBag.NomCaserne = nomCaserne;
                ViewBag.MatriculeCapitaine = matriculeCapitaine;
                ViewBag.ListeCaserne = listeCaserneDTO;

                JsonValue jsonResponseListeFicheIntervetionDTO = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Intervention/ObtenirListeFicheIntervention?nomCaserne=" + nomCaserne + "&matriculeCapitaine=" + matriculeCapitaine);
                List<FicheInterventionDTO> listeFicheInterventionDTO = JsonConvert.DeserializeObject<List<FicheInterventionDTO>>(jsonResponseListeFicheIntervetionDTO.ToString());
                ViewBag.ListeFicheIntervention = listeFicheInterventionDTO;

                return View();

            }
            catch (Exception ex)
            {
                ViewBag.MessageErreurCritique = ex.Message;
                return View();
            }
        }

        /// <summary>
		/// Méthode de service appelé lors de l'action OuvrirFicheIntervention.
		/// Rôles de l'action : 
		///   -Ouvrir une fiche d'intervention.
		/// </summary>
		/// <param name="interventionDTO">Le DTO d'une intervention.</param>
		/// <returns>IActionResult</returns>
        [Route("/Intervention/OuvrirFicheIntervention")]
        [HttpPost]
        public async Task<IActionResult> OuvrirFicheIntervention(string nomCaserne, FicheInterventionDTO fiche)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Intervention/OuvrirFicheIntervention?nomCaserne=" + nomCaserne, fiche);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Intervention", new {nomCaserne =nomCaserne, matriculeCapitaine = fiche.MatriculeCapitaine});
        }

        
    }
}
