using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Configuration.Building;
using UnityEditor;

namespace SpecificationTests
{
	public class BuildingConfigurationValidationTests
	{
		List<BuildingConfiguration> buildingConfigurations;
		
		[SetUp]
		public void Setup()
		{
			this.buildingConfigurations = new List<BuildingConfiguration>();
			string[] buildingConfigGUIDs = AssetDatabase.FindAssets("t:BuildingConfiguration", new string[] {"Assets/Configuration/Buildings"});
			foreach (var buildingConfigGUID in buildingConfigGUIDs)
			{
				this.buildingConfigurations.Add(AssetDatabase.LoadAssetAtPath<BuildingConfiguration>(AssetDatabase.GUIDToAssetPath(buildingConfigGUID)));
			}
		}

		[Test]
		public void ValidateCommonBuildings()
		{
			List<string> testFails = new List<string>();

			const int expectedCount = 5;
			int commonCount = 0;

			Color sharedColor = Color.black;
			string sharedSceneName = string.Empty;

			foreach (var buildingConfiguration in this.buildingConfigurations)
			{
				if (buildingConfiguration.buildingType == BuildingType.COMMON)
				{
					if (commonCount == 0) // First common, set shared!
					{
						sharedColor = buildingConfiguration.mainColor;
						sharedSceneName = buildingConfiguration.popupSceneName;
					}
					else
					{
						if (buildingConfiguration.mainColor != sharedColor)
						{
							testFails.Add(string.Format("Building {0} does not have the same main color as the other common buildings!", buildingConfiguration.name));
						}
						
						if (!buildingConfiguration.popupSceneName.Equals(sharedSceneName))
						{
							testFails.Add(string.Format("Building {0} does not have the same popup as the other common buildings!", buildingConfiguration.name));
						}
					}

					if (buildingConfiguration.maxAmount != -1 && buildingConfiguration.maxAmount < 2)
					{
						testFails.Add(string.Format("Building {0} has max amount {1} but should either be -1 (for infinite) or more than 1!", buildingConfiguration.name, buildingConfiguration.maxAmount));
					}

					if (buildingConfiguration.dimensions.x != 1 || buildingConfiguration.dimensions.y != 1)
					{
						testFails.Add(string.Format("Building {0} has dimensions ({1}, {2}) instead of the expected (1, 1)!", buildingConfiguration.name, buildingConfiguration.dimensions.x, buildingConfiguration.dimensions.y));
					}
					
					commonCount++;
				}
			}

			if (commonCount != expectedCount)
			{
				testFails.Add(string.Format("Found {0} common buildings instead of the expected {1}!", commonCount, expectedCount));
			}
			
			Assert.IsTrue(testFails.Count == 0, string.Join("\n", testFails.ToArray()));
		}

		[Test]
		public void ValidateUniqueBuildings()
		{
			List<string> testFails = new List<string>();

			const int expectedCount = 15;
			int uniqueCount = 0;
			
			Dictionary<Color, string> usedColors = new Dictionary<Color, string>();
			Dictionary<string, string> usedScenes = new Dictionary<string, string>();

			foreach (var buildingConfiguration in this.buildingConfigurations)
			{
				if (buildingConfiguration.buildingType == BuildingType.UNIQUE)
				{
					if (usedColors.ContainsKey(buildingConfiguration.mainColor))
					{
						testFails.Add(string.Format("Building {0} has the same color as {1}!", buildingConfiguration.name, usedColors[buildingConfiguration.mainColor]));
					}
					else
					{
						usedColors.Add(buildingConfiguration.mainColor, buildingConfiguration.name);
					}
					
					if (usedScenes.ContainsKey(buildingConfiguration.popupSceneName))
					{
						testFails.Add(string.Format("Building {0} has the same popup as {1}!", buildingConfiguration.name, usedScenes[buildingConfiguration.popupSceneName]));
					}
					else
					{
						usedScenes.Add(buildingConfiguration.popupSceneName, buildingConfiguration.name);
					}

					if (buildingConfiguration.maxAmount != 1)
					{
						testFails.Add(string.Format("Building {0} has max amount {1} but should be 1!", buildingConfiguration.name, buildingConfiguration.maxAmount));
					}

					if (buildingConfiguration.dimensions.x != 2 || buildingConfiguration.dimensions.y != 2)
					{
						testFails.Add(string.Format("Building {0} has dimensions ({1}, {2}) instead of the expected (2, 2)!", buildingConfiguration.name, buildingConfiguration.dimensions.x, buildingConfiguration.dimensions.y));
					}
					
					uniqueCount++;
				}
			}

			if (uniqueCount != expectedCount)
			{
				testFails.Add(string.Format("Found {0} common buildings instead of the expected {1}!", uniqueCount, expectedCount));
			}
			
			Assert.IsTrue(testFails.Count == 0, string.Join("\n", testFails.ToArray()));
		}
	}
}