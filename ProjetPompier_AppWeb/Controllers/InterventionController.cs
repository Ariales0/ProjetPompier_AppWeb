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
            ViewData["Title"] = "Fiche(s) d'intervention(s)";
            try
            {
                JsonValue jsonResponseListeCaserneDTO = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Caserne/ObtenirListeCaserne");
                List<CaserneDTO> listeCaserneDTO = JsonConvert.DeserializeObject<List<CaserneDTO>>(jsonResponseListeCaserneDTO.ToString());
                List<PompierDTO> listeCapitaineDTO = new List<PompierDTO>();
                if (nomCaserne == null || matriculeCapitaine == 0)
                {
                    nomCaserne = listeCaserneDTO[0].Nom;
                    JsonValue jsonResponseListeCapitaineDTO = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Pompier/ObtenirListeCapitaine?nomCaserne=" + nomCaserne + "&seulementCapitaine=true");
                    listeCapitaineDTO = JsonConvert.DeserializeObject<List<PompierDTO>>(jsonResponseListeCapitaineDTO.ToString());
                    if (listeCapitaineDTO.Count == 0)
                    {
                        ViewBag.MessageErreurCritique = "Aucune caserne!";
                        return View();
                    }

                    if (listeCapitaineDTO.Count == 0)
                    {
                        ViewBag.MessageErreurCritique = "Aucun capitaine dans cette caserne";
                        return View();
                    }
                    matriculeCapitaine = listeCapitaineDTO[0].Matricule;
                }
                else
                {
                    JsonValue jsonResponseListeCapitaineDTO = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Pompier/ObtenirListePompier?nomCaserne=" + nomCaserne + "&seulementCapitaine=true");
                    listeCapitaineDTO = JsonConvert.DeserializeObject<List<PompierDTO>>(jsonResponseListeCapitaineDTO.ToString());
                    if (listeCapitaineDTO.Count == 0)
                    {
                        ViewBag.MessageErreurCritique = "Aucun capitaine dans cette caserne";
                        return View();
                    }
                }

                ViewBag.NomCaserne = nomCaserne;
                ViewBag.MatriculeCapitaine = matriculeCapitaine;
                ViewBag.ListeCaserne = listeCaserneDTO;
                ViewBag.ListeCapitaine = listeCapitaineDTO;

                JsonValue jsonResponseListeFicheIntervetionDTO = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Intervention/ObtenirListeFicheIntervention?nomCaserne=" + nomCaserne + "&matriculeCapitaine=" + matriculeCapitaine);
                List<FicheInterventionDTO> listeFicheInterventionDTO = JsonConvert.DeserializeObject<List<FicheInterventionDTO>>(jsonResponseListeFicheIntervetionDTO.ToString());
                ViewBag.ListeFicheIntervention = listeFicheInterventionDTO;

                if (listeFicheInterventionDTO.Count == 0)
                {
                    ViewBag.Message = "Aucune fiche d'intervention pour ce capitaine";
                }
                return View();

            }
            catch (Exception)
            {
                JsonValue jsonResponseListeCaserneDTO = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Caserne/ObtenirListeCaserne");
                List<CaserneDTO> listeCaserneDTO = JsonConvert.DeserializeObject<List<CaserneDTO>>(jsonResponseListeCaserneDTO.ToString());

                if (ViewBag.NomCaserne != null)
                {
                    try
                    {
                        JsonValue jsonResponseListeCapitaineDTO = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Pompier/ObtenirListePompier?nomCaserne=" + nomCaserne + "&seulementCapitaine=true");
                        List<PompierDTO> listeCapitaineDTO = JsonConvert.DeserializeObject<List<PompierDTO>>(jsonResponseListeCapitaineDTO.ToString());
                        ViewBag.ListeCapitaine = listeCapitaineDTO;

                        if (listeCapitaineDTO.Count != 0)
                        {
                            matriculeCapitaine = listeCapitaineDTO[0].Matricule;
                            ViewBag.MatriculeCapitaine = matriculeCapitaine;
                            JsonValue jsonResponseListeFicheIntervetionDTO = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Intervention/ObtenirListeFicheIntervention?nomCaserne=" + nomCaserne + "&matriculeCapitaine=" + matriculeCapitaine);
                            List<FicheInterventionDTO> listeFicheInterventionDTO = JsonConvert.DeserializeObject<List<FicheInterventionDTO>>(jsonResponseListeFicheIntervetionDTO.ToString());
                            if (listeFicheInterventionDTO.Count == 0)
                            {
                                ViewBag.Message = "Aucune fiche d'intervention pour ce capitaine";
                            }
                            ViewBag.ListeFicheIntervention = listeFicheInterventionDTO;
                        }
                        else
                        {
                            ViewBag.MessageErreurCritique = "Aucun capitaine dans cette caserne";
                        }
                    }
                    catch (Exception e)
                    {
                        ViewBag.MessageErreurCritique = e.Message;
                    }
                }
                else
                {
                    JsonValue jsonResponseListeCapitaineDTO = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Pompier/ObtenirListePompier?nomCaserne=" + nomCaserne + "&seulementCapitaine=true");
                    List<PompierDTO> listeCapitaineDTO = JsonConvert.DeserializeObject<List<PompierDTO>>(jsonResponseListeCapitaineDTO.ToString());
                    ViewBag.ListeCapitaine = listeCapitaineDTO;

                    if (listeCapitaineDTO.Count == 0)
                    {
                        ViewBag.MessageErreurCritique = "Aucun capitaine dans cette caserne";
                    }
                    else
                    {
                        nomCaserne = listeCaserneDTO[0].Nom;
                        matriculeCapitaine = listeCapitaineDTO[0].Matricule;

                        ViewBag.NomCaserne = nomCaserne;
                        ViewBag.MatriculeCapitaine = matriculeCapitaine;

                        ViewBag.ListeCaserne = listeCaserneDTO;
                        ViewBag.ListeCapitaine = listeCapitaineDTO;

                        JsonValue jsonResponseListeFicheIntervetionDTO = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Intervention/ObtenirListeFicheIntervention?nomCaserne=" + nomCaserne + "&matriculeCapitaine=" + matriculeCapitaine);
                        List<FicheInterventionDTO> listeFicheInterventionDTO = JsonConvert.DeserializeObject<List<FicheInterventionDTO>>(jsonResponseListeFicheIntervetionDTO.ToString());
                        ViewBag.ListeFicheIntervention = listeFicheInterventionDTO;

                        if (listeFicheInterventionDTO.Count == 0)
                        {
                            ViewBag.Message = "Aucune fiche d'intervention pour ce capitaine";
                        }
                    }
                }
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
        [Route("/Intervention/OuvrirFicheIntervention")]
        [HttpPost]
        public async Task<IActionResult> OuvrirFicheIntervention([FromForm] string nomCaserne, [FromForm]FicheInterventionDTO ficheInterventionDTO)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Intervention/OuvrirFicheIntervention?nomCaserne=" + nomCaserne, ficheInterventionDTO);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreurCritique = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Intervention", new { nomCaserne, matriculeCapitaine = ficheInterventionDTO.MatriculeCapitaine });
        }

        /// <summary>
		/// Action FormulaireModifierIntervention.
		/// Permet d'afficher le formulaire pour la modification d'une fiche d'intervention.
		/// </summary> 
		/// <param name="nomCaserne">Nom de la caserne.</param>
		/// <param name="matriculeCapitaine">Matricule du pompier capitaine en charge de l'intervention.</param>
		/// <param name="dateDebut">Date du debut de l'intervention.</param>
		/// <returns>async Task<IActionResult></returns>
		[Route("/EnseignantController/FormulaireModifierEnseignant")]
        [HttpGet]
        public async Task<IActionResult> FormulaireModifierEnseignant([FromQuery] string nomCaserne, [FromQuery] int matriculeCapitaine, [FromQuery] string dateDebut)
        {
            try
            {
                JsonValue reponseObtenirFicheIntervention = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Intervention/ObtenirIntervention?nomCaserne=" + nomCaserne + "&matriculeCapitaine=" + matriculeCapitaine + "&dateIntervention=" + dateDebut);
                FicheInterventionDTO ficheInterventionDTO = JsonConvert.DeserializeObject<FicheInterventionDTO>(reponseObtenirFicheIntervention.ToString());

                ViewBag.NomCaserne = nomCaserne;
                ViewBag.MatriculeCapitaine = matriculeCapitaine;

                return View(ficheInterventionDTO);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreurCritique = e.Message;
                return RedirectToAction("Index");
            }

        }


    }
}
