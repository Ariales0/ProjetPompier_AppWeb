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
            JsonValue jsonResponseListeCaserneDTO = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Caserne/ObtenirListeCaserne");
            List<CaserneDTO> listeCaserneDTO = JsonConvert.DeserializeObject<List<CaserneDTO>>(jsonResponseListeCaserneDTO.ToString());

            try
            {
                if (nomCaserne == null || matriculeCapitaine == 0)
                {
                    nomCaserne = listeCaserneDTO[0].Nom;
                    JsonValue jsonResponseListePompierDTO = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Pompier/ObtenirListePompier?nomCaserne=" + nomCaserne + "&seulementCapitaine=true");
                    List<PompierDTO> listePompierDTO = JsonConvert.DeserializeObject<List<PompierDTO>>(jsonResponseListePompierDTO.ToString());

                    if (listePompierDTO.Count == 0)
                    {
                        ViewBag.MessageErreurCritique = "Aucun capitaine dans cette caserne";
                        return View();
                    }
                    matriculeCapitaine = listePompierDTO[0].Matricule;
                }
                ViewBag.NomCaserne = nomCaserne;
                ViewBag.MatriculePompier = matriculeCapitaine;

            }


            jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Pompier/ObtenirListePompier?nomCaserne=" + caserneDTO.Nom + "&seulementCapitaine=" + true) ;
            List<PompierDTO> listePompier = JsonConvert.DeserializeObject<List<PompierDTO>>(jsonResponse.ToString());
            ViewBag.listePompier = listePompier;
            if(matriculeCapitaine == 0 && listePompier.Count > 0)
            {
				matriculeCapitaine = listePompier[0].Matricule;
			}
            ViewBag.MatriculeCapitaine = matriculeCapitaine;

            foreach (PompierDTO pompier in listePompier)
            {
                if(pompier.Matricule == matriculeCapitaine)
                {
                    ViewBag.NomPrenomCapitaine = pompier.Prenom + " " + pompier.Nom;
                }
            }
            
            jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Intervention/ObtenirListeFicheIntervention?nomCaserne=" + caserneDTO.Nom + "&matriculeCapitaine=" + matriculeCapitaine);
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
