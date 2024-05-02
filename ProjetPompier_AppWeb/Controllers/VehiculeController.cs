using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetPompier_AppWeb.Logics.Models;
using ProjetPompier_AppWeb.Utils;
using System.Json;

namespace ProjetPompier_AppWeb.Controllers
{
    public class VehiculeController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <returns>Retourne l'index</returns>
        [Route("Vehicule")]
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string nomCaserne)
        {
            try
            {
                // Appeler le service web pour obtenir la liste des casernes
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Caserne/ObtenirListeCaserne");
                List<CaserneDTO> listeCaserneDTO = JsonConvert.DeserializeObject<List<CaserneDTO>>(jsonResponse.ToString());
                ViewBag.ListeCaserne = listeCaserneDTO;

                if (string.IsNullOrEmpty(nomCaserne))
                {
                    nomCaserne = listeCaserneDTO.FirstOrDefault()?.Nom; // Utiliser le premier nom de caserne si aucun n'est spécifié
                }

                // Appeler le service web pour obtenir la liste des Vehicules
                jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Vehicule/ObtenirListeVehicule?nomCaserne=" + nomCaserne);
                List<VehiculeDTO> listeVehiculeDTO = JsonConvert.DeserializeObject<List<VehiculeDTO>>(jsonResponse.ToString());
                ViewBag.ListeVehicule = listeVehiculeDTO;
                ViewBag.NomCaserne = nomCaserne;

                // Appeler le service web pour obtenir la liste des type de véhicule
                jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/TypesVehicule/ObtenirListeTypesVehicule");
                List<TypeVehiculeDTO> listeTypeVehiculeDTO = JsonConvert.DeserializeObject<List<TypeVehiculeDTO>>(jsonResponse.ToString());
                ViewBag.ListeTypeVehicule = listeTypeVehiculeDTO;
            }

            catch (Exception e)
            {
                ViewBag.MessageErreurCritique = e.Message;
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }

            return View(); // Retourner la vue
        }

        /// <summary>
        /// Méthode de service appelé lors de l'action AjouterVehicule.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="vehicule">Le DTO du vehicule à ajouter</param>
        /// <returns>Retourne l'index vehicule</returns>
        [Route("Vehicule/AjouterVehicule")]
        [HttpPost]
        public async Task<IActionResult> AjouterVehicule(string nomCaserne, VehiculeDTO vehicule)
        {
            // Appeler le service web pour ajouter un vehicule
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Vehicule/AjouterVehicule?nomCaserne=" + nomCaserne, vehicule);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreurCritique = e.Message;
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }
            // Rediriger vers l'index vehicule
            return RedirectToAction("Index", "Vehicule", new { nomCaserne });
        }

        /// <summary>
        /// Méthode de service appelé lors de l'action FormulaireModifierVehicule.
        /// </summary>
        /// <param name="nomCaserne">Le nom de la caserne</param>
        /// <param name="vinVehicule">Le vin du véhicule à modifier</param>
        /// <returns>Retourne le formulaire modifier vehicule</returns>
        [Route("Vehicule/FormulaireModifierVehicule")]
        [HttpGet]
        public async Task<IActionResult> FormulaireModifierVehicule([FromQuery] string nomCaserne, [FromQuery] string vinVehicule)
        {
            try
            {
                // Appeler le service web pour obtenir un véhicule
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Vehicule/ObtenirVehicule?nomCaserne=" + nomCaserne + "&vinVehicule=" + vinVehicule);
                VehiculeDTO vehiculeDTO = JsonConvert.DeserializeObject<VehiculeDTO>(jsonResponse.ToString());
                // Appeler le service web pour obtenir la liste des type de véhicule
                jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/TypesVehicule/ObtenirListeTypesVehicule");
                List<TypeVehiculeDTO> listeTypeVehiculeDTO = JsonConvert.DeserializeObject<List<TypeVehiculeDTO>>(jsonResponse.ToString());
                ViewBag.ListeTypeVehicule = listeTypeVehiculeDTO;
                ViewBag.NomCaserne = nomCaserne;
                return View(vehiculeDTO);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
                return RedirectToAction("Index", "Vehicule", new { nomCaserne });
            }
        }

        /// <summary>
        ///  Méthode de service appelé lors de l'action Modifiervehicule.
        /// </summary>
        /// <param name="nomCaserne">Nom de la caserne</param>
        /// <param name="vehicule">Object véhicule modifié</param>
        /// <returns>Retourne la page vehicule</returns>
        [Route("Vehicule/ModifierVehicule")]
        [HttpPost]
        public async Task<IActionResult> ModifierVehicule(string nomCaserne, VehiculeDTO vehicule)
        {
            // Appeler le service web pour modifier un vehicule
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Vehicule/ModifierVehicule?nomCaserne=" + nomCaserne, vehicule);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreurCritique = e.Message;
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }
            // Rediriger vers l'index vehicule
            return RedirectToAction("Index", "Vehicule", new { nomCaserne });
        }


        /// <summary>
        ///  Méthode de service appelé lors de l'action SupprimmerVehicule.
        /// </summary>
        /// <param name="nomCaserne">Nom de la caserne</param>
        /// <param name="vinVehicule">Matricule du vehicule</param>
        /// <returns>Retourne la page vehicule</returns>
        [Route("Vehicule/SupprimerVehicule")]
        [HttpPost]
        public async Task<IActionResult> SupprimmerVehicule(string nomCaserne, string vinVehicule)
        {
            // Appeler le service web pour supprimer un vehicule
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Vehicule/SupprimerVehicule?nomCaserne=" + nomCaserne + "&vinVehicule=" + vinVehicule, null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreurCritique = e.Message;
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }
            // Rediriger vers l'index vehicule
            return RedirectToAction("Index", "Vehicule", new {  nomCaserne });
        }

        [Route("Vehicule/ViderListeVehicule")]
        [HttpPost]
        public async Task<IActionResult> ViderListeVehicule(string nomCaserne)
        {
            // Appeler le service web pour vider la liste des vehicule
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Vehicule/ViderListeVehicules?nomCaserne=" + nomCaserne, null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreurCritique = e.Message;
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }
            // Rediriger vers l'index vehicule
            return RedirectToAction("Index", "Vehicule", new { nomCaserne });
        }

    }
}
