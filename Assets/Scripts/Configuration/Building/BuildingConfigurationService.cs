using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Configuration.Building
{
	public class BuildingConfigurationService : IBuildingConfigurationService
	{
		// These should never change at runtime, BuildingConfiguration is a pure data class and the instances are all known at startup.
		// I'm using two dictionaries here because I have constant lookup time then, and they are only initialised once and then not modified.
		readonly Dictionary<string, BuildingConfiguration> buildings = new Dictionary<string, BuildingConfiguration>();
		readonly Dictionary<BuildingType, List<BuildingConfiguration>> buildingsByType = new Dictionary<BuildingType, List<BuildingConfiguration>>();
		
		// Buildings should always exist if queried here, if they don't, something went wrong so show an assert
		public void UpdateConfiguration(BuildingLibrary library)
		{
			this.buildings.Clear();
			this.buildingsByType.Clear();
			
			foreach (var buildingConfig in library.buildingConfigurations)
			{
				Assert.IsFalse(this.buildings.ContainsKey(buildingConfig.name), string.Format("Duplicate building config asset name {0}, this is not supported!", buildingConfig.name));
				this.buildings.Add(buildingConfig.name, buildingConfig);

				if (this.buildingsByType.ContainsKey(buildingConfig.buildingType))
				{
					this.buildingsByType[buildingConfig.buildingType].Add(buildingConfig);
				}
				else
				{
					this.buildingsByType.Add(buildingConfig.buildingType, new List<BuildingConfiguration>() {buildingConfig});
				}
			}
		}

		public BuildingConfiguration GetBuilding(string name)
		{
			//Assert.IsTrue(this.buildings.ContainsKey(name), string.Format("No building configuration of name {0} exists!", name));
			Assert.IsTrue(this.buildings.ContainsKey(name), "No building configuration exists!");

			return this.buildings[name];
		}
		
		// There could be empty categories, so let's not trigger an assert here, but instead just return an empty list
		public List<BuildingConfiguration> GetBuildingsOfType(BuildingType type)
		{
			if (this.buildingsByType.ContainsKey(type))
			{
				return this.buildingsByType[type];
			}
			
			return new List<BuildingConfiguration>();
		}
	}
}