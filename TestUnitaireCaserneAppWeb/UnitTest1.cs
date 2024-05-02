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
  //      /// <summary>
  //      /// Test fonctionnel de la fonction Idex du controleur CaserneController
  //      /// </summary>
  //      [Fact]
  //      public void TestFonctionnelCaserne()
  //      {
  //          CaserneDTO caserneDTO = new CaserneDTO("Caserne Test", "Adresse Test", "Ville Test", "Province Test","Tel Test");
  //          CaserneController controleurCaserne = new CaserneController();

  //          controleurCaserne.AjouterCaserne(caserneDTO);

  //          ViewResult resultatCaserne = (ViewResult)controleurCaserne.Index().Result;

  //          List<CaserneDTO> listeCaserneDansBDD = new List<CaserneDTO>((List<CaserneDTO>)resultatCaserne.ViewData["ListeCaserne"]);

  //          //Voir si la derniere caserne de la liste est Caserne Test

  //          Assert.Equal(listeCaserneDansBDD[(listeCaserneDansBDD.Count)-1].Nom, caserneDTO.Nom);

  //          controleurCaserne.SupprimerCaserne(caserneDTO.Nom);

  //      }

  ////      [Fact]
  ////      public void TestFoncitonnelIntervention()
  ////      {
           
  ////          DateTime date = DateTime.Now;
  ////          FicheInterventionDTO interventionDTO;
  ////          interventionDTO = new FicheInterventionDTO(date.ToString(), "Adresse Test", "Type Test", "Resume Test", 3);
            

  ////          InterventionController controleurIntervention = new InterventionController();
			
		////	controleurIntervention.OuvrirFicheIntervention("Caserne Test", interventionDTO);

		////	ViewResult resultatIntervention = (ViewResult)controleurIntervention.Index("Caserne Test", 3).Result;

		////	List<FicheInterventionDTO> listeInterventionDansBDD = new List<FicheInterventionDTO>((List<FicheInterventionDTO>)resultatIntervention.ViewData["ListeIntervention"]);

  ////          Assert.Equal(listeInterventionDansBDD[(listeInterventionDansBDD.Count) - 1].DateTemps, interventionDTO.DateTemps);



		////}

  //      [Fact]
  //      public void TestFonctionnelPompier()
  //      {
  //          string nomCaserne = "Test";
  //          PompierDTO pompierDTO = new PompierDTO(3, "Grade Test", "Nom Test", "Prenom Test");
  //          PompierController controleurPompier = new PompierController();

  //          controleurPompier.AjouterPompier(nomCaserne ,pompierDTO);

  //          ViewResult resultatPompier = (ViewResult)controleurPompier.Index("Caserne Test", 3).Result;

  //          List<PompierDTO> listePompierDansBDD = new List<PompierDTO>((List<PompierDTO>)resultatPompier.ViewData["listePompier"]);

  //          Assert.Equal(listePompierDansBDD[(listePompierDansBDD.Count) - 1].Nom, pompierDTO.Nom);

  //          controleurPompier.SupprimerPompier(nomCaserne ,pompierDTO.Matricule);

  //      }

  //      [Fact]
  //      public void TestFonctionnelGrade()
  //      {
  //          GradeDTO gradeDTO = new GradeDTO("Grade Test");
  //          GradeController controleurGrade = new GradeController();

  //          controleurGrade.AjouterGrade(gradeDTO);

  //          ViewResult resultatGrade = (ViewResult)controleurGrade.Index().Result;

  //          List<GradeDTO> listeGradeDansBDD = new List<GradeDTO>((List<GradeDTO>)resultatGrade.ViewData["ListeGrade"]);

  //          Assert.Equal(listeGradeDansBDD[(listeGradeDansBDD.Count) - 1].Description, gradeDTO.Description);

  //          controleurGrade.SupprimerGrade(gradeDTO.Description);

  //      }

        [Fact]
        public void TestFonctionnelTypeVehicule()
        {
            TypeVehiculeDTO typeVehiculeTest = new TypeVehiculeDTO(420,"Type vehicule test",420);
            TypeVehiculeController controleurTypeVehicule = new TypeVehiculeController();

            controleurTypeVehicule.AjouterTypeVehicule(typeVehiculeTest);

            ViewResult resultatTypeVehicule = (ViewResult)controleurTypeVehicule.Index().Result;

            List<TypeVehiculeDTO> listeTypeVehiculeDansBDD = new List<TypeVehiculeDTO>((List<TypeVehiculeDTO>)resultatTypeVehicule.ViewData["ListeTypeVehicule"]);

            Assert.Equal(listeTypeVehiculeDansBDD[(listeTypeVehiculeDansBDD.Count) - 1].Code, typeVehiculeTest.Code);

            controleurTypeVehicule.SupprimerTypeVehicule(typeVehiculeTest.Code);

        }
    }
}