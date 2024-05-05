using ProjetPompier_API.Logics.Models;

/// <summary>
/// Namespace pour les classe de type DTOs.
/// </summary>
namespace ProjetPompier_API.Logics.DTOs
{
    /// <summary>
    /// Classe représentant le DTO d'une équipe.
    /// </summary>
    public class EquipeDTO
    {
        #region Proprietes
        /// <summary>
        /// Propriété représentant le code de l'équipe
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Propriété représentant la liste des pompiers solicités dans l'intervention
        /// </summary>
        public List<PompierDTO> ListePompierEquipe { get; set; }

        /// <summary>
        /// Propriété représentant le vin du véhicule solicité pour l'intervention.
        /// </summary>
        public string VinVehicule { get; set; }

        #endregion Proprietes

        #region Constructeurs
        /// <summary>
        /// Constructeur vide.
        /// </summary>
        public EquipeDTO() { }

        /// <summary>
        /// Constructeur avec paramètres.
        /// </summary>
        /// <param name="codeEquipe">Le code de l'équipe.</param>
        /// <param name="listePompierEquipe">Liste des pompiers de l'équipe.</param>
        /// /// <param name="vinVehiculeEquipe">Le vin du véhicule</param>
        public EquipeDTO(int codeEquipe = 0, List<PompierDTO> listePompierEquipe = null, string vinVehiculeEquipe = "")
        {
            Code = codeEquipe;
            ListePompierEquipe = listePompierEquipe ?? new List<PompierDTO>();
            VinVehicule = vinVehiculeEquipe;
        }


        /// <summary>
        /// Constructeur avec le modèle EquipeModel en paramètre.
        /// </summary>
        /// <param name="equipe">L'objet du modèle EquipeModel.</param>
        public EquipeDTO(EquipeModel equipe)
        {
            Code = equipe.Code;
            ListePompierEquipe = equipe.ListePompierEquipe;
        }

        #endregion Constructeurs
    }
}
