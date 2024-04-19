using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetPompier_AppWeb.Logics.Models;
using ProjetPompier_AppWeb.Utils;
using System.Json;

namespace ProjetPompier_AppWeb.Controllers
{
    public class PompierController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="seulementCapitaine">Filtre de l'affichage</param>
        /// <returns>Retourne l'index</returns>
        [Route("Pompier")]
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string nomCaserne, [FromQuery] string seulementCapitaine)
        {
            try
            { 
                // Appeler le service web pour obtenir la liste des casernes
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Caserne/ObtenirListeCaserne");
                List<CaserneDTO> listeCaserneDTO = JsonConvert.DeserializeObject<List<CaserneDTO>>(jsonResponse.ToString());
                ViewBag.ListeCaserne = listeCaserneDTO;

                if (string.IsNullOrEmpty(nomCaserne) && string.IsNullOrEmpty(seulementCapitaine))
                {
                    nomCaserne = listeCaserneDTO.FirstOrDefault()?.Nom; // Utiliser le premier nom de caserne si aucun n'est spécifié
                    seulementCapitaine = "false"; // Utiliser la valeur par défaut si rien n'est spécifié
                }

                // Appeler le service web pour obtenir la liste des pompiers
                jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Pompier/ObtenirListePompier?nomCaserne=" + nomCaserne + "&seulementCapitaine=" + seulementCapitaine);
                List<PompierDTO> listePompierDTO = JsonConvert.DeserializeObject<List<PompierDTO>>(jsonResponse.ToString());
                ViewBag.ListePompier = listePompierDTO;
                ViewBag.NomCaserne = nomCaserne;
                ViewBag.SeulementCapitaine = seulementCapitaine;

                // Appeler le service web pour obtenir la liste des grades
                jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Grade/ObtenirListeGrade");
                List<GradeDTO> listeGradeDTO = JsonConvert.DeserializeObject<List<GradeDTO>>(jsonResponse.ToString());
                ViewBag.ListeGrade = listeGradeDTO;
            }

            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }

            return View(); // Retourner la vue
        }

        /// <summary>
        /// Méthode de service appelé lors de l'action AjouterPompier.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="pompier">Le pompier a ajouter</param>
        /// <returns>Retourne l'index pompier</returns>
        [Route("Pompier/AjouterPompier")]
        [HttpPost]
        public async Task<IActionResult> AjouterPompier(string nomCaserne, PompierDTO pompier)
        {
            // Appeler le service web pour ajouter un pompier
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Pompier/AjouterPompier?nomCaserne=" + nomCaserne, pompier);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }
            // Rediriger vers l'index pompier
            return RedirectToAction("Index", "Pompier", new { nomCaserne = nomCaserne });
        }

        /// <summary>
        /// Méthode de service appelé lors de l'action ModifierPompier.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="matriculePompier">La matricule du pompier</param>
        /// <returns>Retourne le formulaire modifier pompier</returns>
        [Route("Pompier/FormulaireModifierPompier")]
        [HttpGet]
        public async Task<IActionResult> FormulaireModifierPompier([FromQuery] string nomCaserne, [FromQuery] int matriculePompier)
        {
            try
            {
                // Appeler le service web pour obtenir un pompier
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Pompier/ObtenirPompier?nomCaserne=" + nomCaserne + "&matriculePompier=" + matriculePompier);
                PompierDTO pompierDTO = JsonConvert.DeserializeObject<PompierDTO>(jsonResponse.ToString());
                ViewBag.NomCaserne = nomCaserne;
                return View(pompierDTO);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
                return RedirectToAction("Index", "Pompier", new { nomCaserne = nomCaserne });
            }
        }

        [Route("Pompier/ModifierPompier")]
        [HttpPost]
        public async Task<IActionResult> ModifierPompier(string nomCaserne, PompierDTO pompier)
        {
            // Appeler le service web pour modifier un pompier
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Pompier/ModifierPompier?nomCaserne=" + nomCaserne, pompier);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }
            // Rediriger vers l'index pompier
            return RedirectToAction("Index", "Pompier", new { nomCaserne = nomCaserne });
        }


        /// <summary>
        ///  Méthode de service appelé lors de l'action ModifierPompier.
        /// </summary>
        /// <param name="nomCaserne">Nom de la caserne</param>
        /// <param name="matriculePompier">Matricule du pompier</param>
        /// <returns>Retourne la page pompier</returns>
        [Route("Pompier/SupprimerPompier")]
        [HttpPost]
        public async Task<IActionResult> SupprimerPompier(string nomCaserne, int matriculePompier)
        {
            // Appeler le service web pour supprimer un pompier
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Pompier/SupprimerPompier?nomCaserne=" + nomCaserne + "&matriculePompier=" + matriculePompier, null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }
            // Rediriger vers l'index pompier
            return RedirectToAction("Index", "Pompier", new { nomCaserne = nomCaserne });
        }

    }
}
