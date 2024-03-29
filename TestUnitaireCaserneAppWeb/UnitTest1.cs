global using Xunit;
using Microsoft.AspNetCore.Mvc;
using ProjetPompier_AppWeb.Controllers;
using ProjetPompier_AppWeb.Logics.Models;

/// <summary>
/// Namespace pour la classe de test fonctionnel
/// </summary>
namespace TestUnitaireCaserneAppWeb
{
    /// <summary>
    /// Classe représentant un test fonctionnel du controleu de l'AppWeb.
    /// </summary>
    public class UnitTest1
    {
        /// <summary>
        /// Test fonctionnel de la fonction Idex du controleur CaserneController
        /// </summary>
        [Fact]
        public void TestFonctionnel()
        {
            CaserneDTO caserneDTO = new CaserneDTO("Caserne Test", "Adresse Test", "Ville Test", "Province Test","Tel Test");
            CaserneController controleurCaserne = new CaserneController();

            controleurCaserne.AjouterCaserne(caserneDTO);

            ViewResult resultatCaserne = (ViewResult)controleurCaserne.Index().Result;

            List<CaserneDTO> listeCaserneDansBDD = new List<CaserneDTO>((List<CaserneDTO>)resultatCaserne.ViewData["ListeCaserne"]);

            //Voir si la derniere caserne de la liste est Caserne Test

            Assert.Equal(listeCaserneDansBDD[(listeCaserneDansBDD.Count)-1].Nom, caserneDTO.Nom);

            controleurCaserne.SupprimerCaserne(caserneDTO.Nom);

        }
    }
}