


using ProjetPompier_API.Logics.Models;

/// <summary>
/// Namespace pour les classe de type DTOs.
/// </summary>
namespace ProjetPompier_API.Logics.DTOs
{
    /// <summary>
    /// Classe représentant le DTO d'une fiche d'intervention.
    /// </summary>
    public class FicheInterventionDTO
    {

        #region Proprietes

        /// <summary>
        /// Propriété représentant la date et l'heure du début l'intervention.
        /// </summary>
        public string DateDebut { get; set; }

        /// <summary>
        /// Propriété représentant la date et l'heure de la fin de l'intervention.
        /// </summary>
        public string DateFin { get; set; }

        /// <summary>
        /// Propriété représentant l'adresse de l'intervention'.
        /// </summary>
        public string Adresse { get; set; }

        /// <summary>
        /// Propriété représentant le code du type d'intervention.
        /// </summary>
        public int CodeTypeIntervention { get; set; }

        /// <summary>
        /// Propriété représentant le resumé de l'intervention.
        /// </summary>
        public string Resume { get; set; }

        /// <summary>
        /// Propriété représentant le matricule du capitaine.
        /// </summary>
        public int MatriculeCapitaine { get; set; }

        #endregion Proprietes

        #region Constructeurs

        /// <summary>
        /// Constructeur vide.
        /// </summary>

        public FicheInterventionDTO() { }

        /// <summary>
        /// Constructeur avec paramètres.
        /// </summary>
        /// <param name="dateDebut">Date et heure de l'intervention</param>
        /// <param name="dateFin">Date et heure de l'intervention</param>
        /// <param name="adresse">Adresse de l'intervention</param>
        /// <param name="typeIntervention">Type d'intervention</param>
        /// <param name="resume">Resumé de l'intervention</param>
        /// <param name="matriculeCapitaine">Matricule du pompier capitaine</param>
        /// <param name="vinVehicule">Vin du véhicule solicité pour l'intervention</param>
        public FicheInterventionDTO(string dateDebut = "1999-01-01 00:00:00", string dateFin = null, string adresse = "", int codeTypeIntervention = 000, string resume = "", int matriculeCapitaine = 000000, string vinVehicule="")

        {
            DateDebut = dateDebut;
            DateFin = dateFin;
            Adresse = adresse;
            CodeTypeIntervention = codeTypeIntervention;
            Resume = resume;
            MatriculeCapitaine = matriculeCapitaine;
            VinVehicule = vinVehicule;
        }

        /// <summary>
        /// Constructeur avec le modèle FicheInterventionModel en paramètre.
        /// </summary>
        /// <param name="laFiche">Le model fiche</param>
        public FicheInterventionDTO(FicheInterventionModel laFiche)

        {
            DateDebut = laFiche.DateDebut;
            DateFin = laFiche.DateFin;
            Adresse = laFiche.Adresse;
            CodeTypeIntervention = laFiche.CodeTypeIntervention;
            Resume = laFiche.Resume;
            MatriculeCapitaine = laFiche.MatriculeCapitaine;
            VinVehicule = laFiche.VinVehicule;
        }

        #endregion Constructeurs

    }

}
