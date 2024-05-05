using ProjetPompier_AppWeb.Logics.Models;
/// <summary>
/// Namespace pour les classe de type DTOs.
/// </summary>

namespace ProjetPompier_AppWeb.Logics.Models
{
    /// <summary>
    /// Classe représentant le DTO d'un type d'intervention.
    /// </summary>
    public class TypeInterventionDTO
    {
        #region Proprietes

        /// <summary>
        /// Propriété représentant la description du type d'intervention.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Propriété représentant le code du type d'intervention.
        /// </summary>
        public int Code { get; set; }

        #endregion Proprietes

        #region Constructeurs

        /// <summary>
        /// Constructeur vide.
        /// </summary>
        public TypeInterventionDTO()
        {
        }

        /// <summary>
        /// Constructeur avec paramètres.
        /// </summary>
        /// <param name="codeTypeIntervention">Le code</param>
        /// <param name="descriptionTypeIntervention">Le type</param>
        public TypeInterventionDTO(int codeTypeIntervention = 0000, string descriptionTypeIntervention = "")
        {
            Code = codeTypeIntervention;
            Description = descriptionTypeIntervention;
        }

        
        #endregion Constructeurs
    }
}
