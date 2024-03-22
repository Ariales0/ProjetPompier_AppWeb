
/// <summary>
/// Namespace pour les classe de type DTOs.
/// </summary>
namespace ProjetPompier_API.Logics.Models
{
    /// <summary>
    /// Classe représentant le DTO d'une caserne.
    /// </summary>
    public class CaserneDTO
    {
        #region Proprietes
        /// <summary>
        /// Propriété représentant le nom d'une caserne.
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Propriété représentant l'adresse d'une caserne.
        /// </summary>
        public string Adresse { get; set; }

        /// <summary>
        /// Propriété représentant la ville d'une caserne.
        /// </summary>
        public string Ville { get; set; }

        /// <summary>
        /// Propriété représentant la province d'une caserne.
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// Propriété représenant le téléphone d'une caserne.
        /// </summary>
        public string Telephone { get; set; }

        #endregion Proprietes

        #region Constructeurs
        /// <summary>
        /// Constructeur vide.
        /// </summary>
        public CaserneDTO() { }

        /// <summary>
        /// Constructeur avec paramètres.
        /// </summary>
        /// <param name="nom">Nom d'une caserne.</param>
        /// <param name="adresse">Adresse d'une caserne.</param>
        /// <param name="ville">Ville d'une caserne.</param>
        /// <param name="province">Province d'une caserne.</param>
        /// <param name="telephone">Téléphone d'une caserne.</param>
        public CaserneDTO(string nom = "", string adresse = "", string ville = "", string province = "", string telephone = "")
        {
            Nom = nom;
            Adresse = adresse;
            Ville = ville;
            Province = province;
            Telephone = telephone;
        }

        #endregion Constructeurs
    }
}
