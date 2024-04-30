
/// <summary>
/// Namespace pour les classe de type DTOs.
/// </summary>
namespace ProjetPompier_AppWeb.Logics.Models
{
    /// <summary>
    /// Classe représentant le DTO d'un type de véhicule.
    /// </summary>
    public class TypeVehiculeDTO
    {
        #region Proprietes

        /// <summary>
        /// Propriété représentant le type du véhicule.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Propriété représentant le code du véhicule.
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Propriété représentant le nombre de personne du véhicule.
        /// </summary>
        public int Personnes { get; set; }

        #endregion Proprietes

        #region Constructeurs

        /// <summary>
        /// Constructeur vide.
        /// </summary>
        public TypeVehiculeDTO()
        {
        }

        /// <summary>
        /// Constructeur avec paramètres.
        /// </summary>
        /// <param name="code">Le code</param>
        /// <param name="type">Le type</param>
        /// <param name="personnes">Le nombre de personnes</param>
        public TypeVehiculeDTO(int code = 0000, string type = "", int personnes = 0)
        {
            Type = type;
            Code = code;
            Personnes = personnes;
        }

        #endregion Constructeurs
    }
}
