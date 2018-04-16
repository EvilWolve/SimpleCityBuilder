using NUnit.Framework;
using System.Collections.Generic;
using Buildings;
using Buildings.Save;
using Buildings.Visual;
using Configuration.Building;
using UnityEngine;
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
                this.GetFakeBuildingConfiguration("Common_1", BuildingType.COMMON, 5),
                this.GetFakeBuildingConfiguration("Common_2", BuildingType.COMMON, -1),
                this.GetFakeBuildingConfiguration("Unique_1", BuildingType.UNIQUE, 1),
            };
            
            this.configService.UpdateConfiguration(library);
        }

        [Test]
        public void TestCanBuildBuildingSingle()
        {
            Building building = new Building(this.configService.GetBuilding("Common_1"));
            
            this.buildingService.PlaceBuilding(building);

            bool canBuildBuilding = this.buildingService.CanBuildBuilding(building.Config);
            Assert.IsTrue(canBuildBuilding);
        }

        [Test]
        public void TestCanBuildBuildingMultiple()
        {
            Building building = new Building(this.configService.GetBuilding("Common_1"));
            
            this.buildingService.PlaceBuilding(building);
            this.buildingService.PlaceBuilding(building);
            this.buildingService.PlaceBuilding(building);

            bool canBuildBuilding = this.buildingService.CanBuildBuilding(building.Config);
            Assert.IsTrue(canBuildBuilding);
        }

        [Test]
        public void TestCannotBuildBuildingSingle()
        {
            Building building = new Building(this.configService.GetBuilding("Unique_1"));
            
            this.buildingService.PlaceBuilding(building);

            bool canBuildBuilding = this.buildingService.CanBuildBuilding(building.Config);
            Assert.IsFalse(canBuildBuilding);
        }

        [Test]
        public void TestCannotBuildBuildingMultiple()
        {
            Building building = new Building(this.configService.GetBuilding("Common_1"));
            
            this.buildingService.PlaceBuilding(building);
            this.buildingService.PlaceBuilding(building);
            this.buildingService.PlaceBuilding(building);
            this.buildingService.PlaceBuilding(building);
            this.buildingService.PlaceBuilding(building);

            bool canBuildBuilding = this.buildingService.CanBuildBuilding(building.Config);
            Assert.IsFalse(canBuildBuilding);
        }

        [Test]
        public void TestCanBuildBuildingInfinite()
        {
            Building building = new Building(this.configService.GetBuilding("Common_2"));
            
            this.buildingService.PlaceBuilding(building);
            this.buildingService.PlaceBuilding(building);
            this.buildingService.PlaceBuilding(building);
            this.buildingService.PlaceBuilding(building);
            this.buildingService.PlaceBuilding(building);

            bool canBuildBuilding = this.buildingService.CanBuildBuilding(building.Config);
            Assert.IsTrue(canBuildBuilding);
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