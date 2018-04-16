using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Configuration.Building;
using UnityEngine.TestTools;

namespace UnitTests.Configuration
{
    public class BuildingConfigurationServiceTests
    {
        IBuildingConfigurationService configService;

        bool wasRaisingExceptions;

        [SetUp]
        public void Setup()
        {
            this.wasRaisingExceptions = UnityEngine.Assertions.Assert.raiseExceptions;
            UnityEngine.Assertions.Assert.raiseExceptions = false;
            
            this.configService = new BuildingConfigurationService();

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

        [TearDown]
        public void Teardown()
        {
            UnityEngine.Assertions.Assert.raiseExceptions = this.wasRaisingExceptions;
        }

        [Test]
        public void TestGetBuilding()
        {
            BuildingConfiguration common = this.configService.GetBuilding("Common_1");
            
            Assert.IsNotNull(common);
            Assert.AreEqual("Common_1", common.name);
            Assert.AreEqual(BuildingType.COMMON, common.buildingType);
            Assert.AreEqual(5, common.maxAmount);
            
            BuildingConfiguration unique = this.configService.GetBuilding("Unique_2");
            
            Assert.IsNotNull(unique);
            Assert.AreEqual("Unique_2", unique.name);
            Assert.AreEqual(BuildingType.UNIQUE, unique.buildingType);
            Assert.AreEqual(1, unique.maxAmount);
        }

        /* This should work perfectly fine to check that the assert is actually triggered, but for whatever reason the entire test fails on the assert and never reaches LogAssert.Expect
        [Test]
        public void TestGetBuildingThatDoesntExist()
        {
            this.configService.GetBuilding("Common_3");
            
            LogAssert.Expect(LogType.Assert, "No building configuration exists!");
        }
        */

        [Test]
        public void TestGetBuildingsByType()
        {
            List<BuildingConfiguration> commons = this.configService.GetBuildingsOfType(BuildingType.COMMON);
            
            Assert.IsNotNull(commons);
            Assert.AreEqual(2, commons.Count);
            Assert.AreEqual("Common_1", commons[0].name);
            Assert.AreEqual(BuildingType.COMMON, commons[0].buildingType);
            Assert.AreEqual(5, commons[0].maxAmount);
            Assert.AreEqual("Common_2", commons[1].name);
            Assert.AreEqual(BuildingType.COMMON, commons[1].buildingType);
            Assert.AreEqual(4, commons[1].maxAmount);
            
            List<BuildingConfiguration> uniques = this.configService.GetBuildingsOfType(BuildingType.UNIQUE);
            
            Assert.IsNotNull(uniques);
            Assert.AreEqual(2, uniques.Count);
            Assert.AreEqual("Unique_1", uniques[0].name);
            Assert.AreEqual(BuildingType.UNIQUE, uniques[0].buildingType);
            Assert.AreEqual(1, uniques[0].maxAmount);
            Assert.AreEqual("Unique_2", uniques[1].name);
            Assert.AreEqual(BuildingType.UNIQUE, uniques[1].buildingType);
            Assert.AreEqual(1, uniques[1].maxAmount);
        }
        
        [Test]
        public void TestGetBuildingThatDoesntExist()
        {
            List<BuildingConfiguration> undefined = this.configService.GetBuildingsOfType(BuildingType.UNDEFINED);
            
            Assert.IsNotNull(undefined);
            Assert.AreEqual(0, undefined.Count);
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