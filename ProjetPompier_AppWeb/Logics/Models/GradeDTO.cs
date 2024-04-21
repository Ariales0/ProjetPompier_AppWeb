namespace ProjetPompier_AppWeb.Logics.Models
{
    /// <summary>
    /// Classe représentant le DTO d'un grade.
    /// </summary>
    public class GradeDTO
    {
        #region Proprietes

        /// <summary>
        /// Propriété représentant la description du grade.
        /// </summary>
        public string Description { get; set; }

        #endregion Proprietes

        #region Constructeurs
        /// <summary>
        /// Constructeur avec paramètres.
        /// </summary>
        /// <param name="description">Description du grade</param>
        public GradeDTO(string description = "")
        {
            Description = description;
        }

        /// <summary>
        /// Constructeur vide.
        /// </summary>
        public GradeDTO()
        {
        }

        #endregion Constructeurs
    }
}