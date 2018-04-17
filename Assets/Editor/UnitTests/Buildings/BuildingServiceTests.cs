using NUnit.Framework;
using System.Collections.Generic;
using Buildings;
using Buildings.Save;
using Buildings.Visual;
using Configuration.Building;
using UnityEngine;
using UnityEngine.TestTools;
using Utilities;

namespace UnitTests.Buildings
{
    public class BuildingServiceTests
    {
        IBuildingService buildingService;
        IBuildingConfigurationService configService;

        [SetUp]
        public void Setup()
        {
            this.buildingService = new BuildingService();
            ServiceLocator.Instance.ProvideService<IBuildingService>(this.buildingService);
            
            this.configService = new BuildingConfigurationService();
            ServiceLocator.Instance.ProvideService<IBuildingConfigurationService>(this.configService);
            
            ServiceLocator.Instance.ProvideService<IBuildingVisualFactory>(new DummyBuildingVisualFactory());

            BuildingLibrary library = this.GetFakeBuildingLibrary();
            library.buildingConfigurations = new BuildingConfiguration[]
            {
                this.GetFakeBuildingConfiguration("Common_1", BuildingType.COMMON, 5, new Vector2Int(1, 1)),
                this.GetFakeBuildingConfiguration("Common_2", BuildingType.COMMON, -1, new Vector2Int(1, 1)),
                this.GetFakeBuildingConfiguration("Unique_1", BuildingType.UNIQUE, 1, new Vector2Int(2, 2)),
            };
            
            this.configService.UpdateConfiguration(library);
        }

        [Test]
        public void TestCanBuildBuildingSingle()
        {
            Building building = this.CreateBuilding("Common_1", 0, 0);
            
            this.buildingService.PlaceBuilding(building);

            bool canBuildBuilding = this.buildingService.CanBuildBuilding(building.Config);
            Assert.IsTrue(canBuildBuilding);
        }

        [Test]
        public void TestCanBuildBuildingMultiple()
        {
            Building building = this.CreateBuilding("Common_1", 0, 0);
            this.buildingService.PlaceBuilding(building);
            
            building = this.CreateBuilding("Common_1", 1, 0);
            this.buildingService.PlaceBuilding(building);
            
            building = this.CreateBuilding("Common_1", 0, 1);
            this.buildingService.PlaceBuilding(building);

            bool canBuildBuilding = this.buildingService.CanBuildBuilding(building.Config);
            Assert.IsTrue(canBuildBuilding);
        }

        [Test]
        public void TestCannotBuildBuildingSingle()
        {
            Building building = this.CreateBuilding("Unique_1", 0, 0);
            this.buildingService.PlaceBuilding(building);

            bool canBuildBuilding = this.buildingService.CanBuildBuilding(building.Config);
            Assert.IsFalse(canBuildBuilding);
        }

        [Test]
        public void TestCannotBuildBuildingMultiple()
        {
            Building building = this.CreateBuilding("Common_1", 0, 0);
            this.buildingService.PlaceBuilding(building);
            
            building = this.CreateBuilding("Common_1", 1, 0);
            this.buildingService.PlaceBuilding(building);
            
            building = this.CreateBuilding("Common_1", 0, 1);
            this.buildingService.PlaceBuilding(building);
            
            building = this.CreateBuilding("Common_1", 1, 1);
            this.buildingService.PlaceBuilding(building);
            
            building = this.CreateBuilding("Common_1", 2, 1);
            this.buildingService.PlaceBuilding(building);

            bool canBuildBuilding = this.buildingService.CanBuildBuilding(building.Config);
            Assert.IsFalse(canBuildBuilding);
        }

        [Test]
        public void TestCanBuildBuildingInfinite()
        {
            Building building = this.CreateBuilding("Common_2", 0, 0);
            this.buildingService.PlaceBuilding(building);
            
            building = this.CreateBuilding("Common_2", 1, 0);
            this.buildingService.PlaceBuilding(building);
            
            building = this.CreateBuilding("Common_2", 0, 1);
            this.buildingService.PlaceBuilding(building);
            
            building = this.CreateBuilding("Common_2", 1, 1);
            this.buildingService.PlaceBuilding(building);
            
            building = this.CreateBuilding("Common_2", 2, 1);
            this.buildingService.PlaceBuilding(building);

            bool canBuildBuilding = this.buildingService.CanBuildBuilding(building.Config);
            Assert.IsTrue(canBuildBuilding);
        }

        [Test]
        public void TestCanPlaceBuilding()
        {
            Building building1 = this.CreateBuilding("Common_1", 0, 0);
            this.buildingService.PlaceBuilding(building1);
            
            Building building2 = this.CreateBuilding("Common_1", 1, 1);

            bool canPlaceBuilding = this.buildingService.CanPlaceBuilding(building2);
            Assert.IsTrue(canPlaceBuilding);
        }

        [Test]
        public void TestCannotPlaceBuilding()
        {
            Building building1 = this.CreateBuilding("Unique_1", 0, 0);
            this.buildingService.PlaceBuilding(building1);
            
            Building building2 = this.CreateBuilding("Common_1", 0, 0);

            bool canPlaceBuilding = this.buildingService.CanPlaceBuilding(building2);
            Assert.IsFalse(canPlaceBuilding);
        }

        [Test]
        public void TestLoadBuildings()
        {
            Building building1 = this.CreateBuilding("Common_2", 0, 0);
            this.buildingService.PlaceBuilding(building1);

            Building building2 = this.CreateBuilding("Unique_1", 1, 1);
            this.buildingService.PlaceBuilding(building2);

            this.buildingService.Save();
            
            // Clear building service
            this.buildingService = new BuildingService();
            ServiceLocator.Instance.ProvideService<IBuildingService>(this.buildingService);
            
            this.buildingService.Load();

            List<Building> storedBuildings = this.buildingService.GetAllBuildings();
            
            Assert.AreEqual(2, storedBuildings.Count);
            Assert.IsTrue(storedBuildings.Contains(building1));
            Assert.IsTrue(storedBuildings.Contains(building2));
        }

        [Test]
        public void TestLoadBuildingsWithoutClearing()
        {
            Building building1 = this.CreateBuilding("Common_2", 0, 0);
            this.buildingService.PlaceBuilding(building1);

            Building building2 = this.CreateBuilding("Unique_1", 1, 1);
            this.buildingService.PlaceBuilding(building2);

            this.buildingService.Save();
            
            this.buildingService.Load();

            List<Building> storedBuildings = this.buildingService.GetAllBuildings();
            
            Assert.AreEqual(2, storedBuildings.Count);
            Assert.IsTrue(storedBuildings.Contains(building1));
            Assert.IsTrue(storedBuildings.Contains(building2));
        }
        
        BuildingLibrary GetFakeBuildingLibrary()
        {
            return ScriptableObject.CreateInstance<BuildingLibrary>();
        }

        BuildingConfiguration GetFakeBuildingConfiguration(string name, BuildingType type, int maxAmount, Vector2Int dimensions)
        {
            BuildingConfiguration result = ScriptableObject.CreateInstance<BuildingConfiguration>();
            result.name = name;
            result.buildingType = type;
            result.maxAmount = maxAmount;
            result.dimensions = dimensions;

            return result;
        }

        Building CreateBuilding(string configName, int x, int y)
        {
            Building building = new Building(this.configService.GetBuilding(configName));
            building.SetGridPosition(new Vector2Int(x, y));

            return building;
        }
    }
}