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
            ViewBag.NomCaserne = nomCaserne;
            JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Caserne/ObtenirCaserne?nomCaserne=" + nomCaserne);
            CaserneDTO caserneDTO = JsonConvert.DeserializeObject<CaserneDTO>(jsonResponse.ToString());
            
            jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Pompier/ObtenirListePompier?nomCaserne=" + caserneDTO.Nom + "&seulementCapitaine=" + true) ;
            List<PompierDTO> listePompier = JsonConvert.DeserializeObject<List<PompierDTO>>(jsonResponse.ToString());
            ViewBag.listePompier = listePompier;
            if(matriculeCapitaine == 0)
            {
				matriculeCapitaine = listePompier[0].Matricule;
			}
			ViewBag.MatriculeCapitaine = matriculeCapitaine;
            
            jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Intervention/ObtenirListeFicheIntervention?nomCaserne=" + caserneDTO.Nom + "&matriceCapitaine=" + matriculeCapitaine);
            ViewBag.ListeIntervention = JsonConvert.DeserializeObject<List<FicheInterventionDTO>>(jsonResponse.ToString());
            
            
            await Task.Delay(0);
            return View();
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
        public async Task<IActionResult> OuvrirFicheIntervention(string nomCaserne, string dateTemps, string typeIntervention, string adresse, string resume, int matriculeCapitaine)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Intervention/OuvrirFicheIntervention?nomCaserne=" + nomCaserne + "&dateTemps=" + dateTemps + "&typeIntervention=" + typeIntervention
                    + "&adresse=" + adresse + "&resume=" + resume + "&matriculeCapitaine=" + matriculeCapitaine, null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Intervention", new {nomCaserne =nomCaserne, matriculeCapitaine = matriculeCapitaine});
        }

        
    }
}
