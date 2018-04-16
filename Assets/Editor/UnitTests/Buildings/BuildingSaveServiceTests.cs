using NUnit.Framework;
using System.Collections.Generic;
using BuildingManagement;
using BuildingManagement.Save;
using BuildingManagement.Visual;
using Configuration.Building;
using UnityEngine;
using Utilities;

namespace UnitTests.Buildings
{
    public class BuildingSaveServiceTests
    {
        IBuildingSaveService buildingSaveService;
        IBuildingConfigurationService configService;

        [SetUp]
        public void Setup()
        {
            this.buildingSaveService = new BuildingSaveService();
            
            this.configService = new BuildingConfigurationService();
            ServiceLocator.Instance.ProvideService<IBuildingConfigurationService>(this.configService);
            
            ServiceLocator.Instance.ProvideService<IBuildingVisualFactory>(new DummyBuildingVisualFactory());

            BuildingLibrary library = this.GetFakeBuildingLibrary();
            library.buildingConfigurations = new BuildingConfiguration[]
            {
                this.GetFakeBuildingConfiguration("Common_1", BuildingType.COMMON, 5),
                this.GetFakeBuildingConfiguration("Common_2", BuildingType.COMMON, 4),
                this.GetFakeBuildingConfiguration("Unique_1", BuildingType.UNIQUE, 1),
                this.GetFakeBuildingConfiguration("Unique_2", BuildingType.UNIQUE, 1),
            };
            
            this.configService.UpdateConfiguration(library);
        }

        [Test]
        public void TestSaveAndLoadBuildings()
        {
            List<Building> buildings = new List<Building>()
            {
                new Building(this.configService.GetBuilding("Common_1")),
                new Building(this.configService.GetBuilding("Unique_2")),
            };

            this.buildingSaveService.SaveBuildings(buildings);

            List<Building> loadedBuildings = this.buildingSaveService.LoadBuildings();

            Assert.IsNotNull (loadedBuildings);
            Assert.AreEqual (buildings, loadedBuildings);
        }

        [Test]
        public void TestLoadBuildingsWithNoData()
        {
            this.buildingSaveService.SaveBuildings(new List<Building>());
            
            List<Building> loadedBuildings = this.buildingSaveService.LoadBuildings();

            Assert.IsNotNull (loadedBuildings);
            Assert.AreEqual (0, loadedBuildings.Count);
        }

        BuildingLibrary GetFakeBuildingLibrary()
        {
            return ScriptableObject.CreateInstance<BuildingLibrary>();
        }

        BuildingConfiguration GetFakeBuildingConfiguration(string name, BuildingType type, int maxAmount)
        {
            BuildingConfiguration result = ScriptableObject.CreateInstance<BuildingConfiguration>();
            result.name = name;
            result.buildingType = type;
            result.maxAmount = maxAmount;

            return result;
        }
    }
}