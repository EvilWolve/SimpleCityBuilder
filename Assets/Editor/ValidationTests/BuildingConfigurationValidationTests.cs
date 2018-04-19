using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Configuration.Building;
using UnityEditor;

namespace ValidationTests
{
	public class BuildingConfigurationValidationTests
	{
		BuildingLibrary buildingLibrary;
		List<BuildingConfiguration> buildingConfigurations;
		
		[SetUp]
		public void Setup()
		{
			this.buildingLibrary = Resources.Load<BuildingLibrary>(BuildingLibrary.RESOURCE_LOCATION);

			this.buildingConfigurations = new List<BuildingConfiguration>();
			string[] buildingConfigGUIDs = AssetDatabase.FindAssets("t:BuildingConfiguration", new string[] {"Assets/Configuration/Buildings"});
			foreach (var buildingConfigGUID in buildingConfigGUIDs)
			{
				this.buildingConfigurations.Add(AssetDatabase.LoadAssetAtPath<BuildingConfiguration>(AssetDatabase.GUIDToAssetPath(buildingConfigGUID)));
			}
		}

		[Test]
		public void ValidateBuildingLibaryLocation()
		{
			Assert.IsNotNull(this.buildingLibrary);
		}

		[Test]
		public void ValidateBuildingLibraryContentsNotNull()
		{
			List<string> testFails = new List<string>();

			for (int i = 0; i < this.buildingLibrary.buildingConfigurations.Length; i++)
			{
				if (this.buildingLibrary.buildingConfigurations[i] == null)
				{
					testFails.Add(string.Format("Building library has null-entry at element {0}!", i));
				}
			}
			
			Assert.IsTrue(testFails.Count == 0, string.Join("\n", testFails.ToArray()));
		}

		[Test]
		public void ValidateBuildingType()
		{
			List<string> testFails = new List<string>();

			foreach (var buildingConfiguration in this.buildingConfigurations)
			{
				if (buildingConfiguration.buildingType == BuildingType.UNDEFINED)
				{
					testFails.Add(string.Format("Building configuration '{0}' has building type Undefined!", buildingConfiguration.name));
				}
			}
			
			Assert.IsTrue(testFails.Count == 0, string.Join("\n", testFails.ToArray()));
		}

		[Test]
		public void ValidateDimensions()
		{
			List<string> testFails = new List<string>();

			foreach (var buildingConfiguration in this.buildingConfigurations)
			{
				if (buildingConfiguration.dimensions.x <= 0 || buildingConfiguration.dimensions.y <= 0)
				{
					testFails.Add(string.Format("Building configuration '{0}' has invalid dimensions!", buildingConfiguration.name));
				}
			}
			
			Assert.IsTrue(testFails.Count == 0, string.Join("\n", testFails.ToArray()));
		}

		[Test]
		public void ValidateReferences()
		{
			List<string> testFails = new List<string>();

			foreach (var buildingConfiguration in this.buildingConfigurations)
			{
				if (buildingConfiguration.icon == null)
				{
					testFails.Add(string.Format("Building configuration '{0}' is missing its icon sprite!", buildingConfiguration.name));
				}
				
				if (buildingConfiguration.prototype == null)
				{
					testFails.Add(string.Format("Building configuration '{0}' is missing its prototype prefab!", buildingConfiguration.name));
				}
				
				if (buildingConfiguration.prefab == null)
				{
					testFails.Add(string.Format("Building configuration '{0}' is missing its visual prefab!", buildingConfiguration.name));
				}
			}
			
			Assert.IsTrue(testFails.Count == 0, string.Join("\n", testFails.ToArray()));
		}

		[Test]
		public void ValidatePopupScenes()
		{
			string[] scenesInBuildNames = System.Array.ConvertAll(EditorBuildSettings.scenes, (scene) =>
			{
				string name = scene.path;
				int filenameIndex = name.IndexOf(".");
				name = name.Substring(0, filenameIndex);

				int lastBracketIndex = name.LastIndexOf("/");
				return name.Substring(lastBracketIndex + 1);
			});
			
			List<string> testFails = new List<string>();

			foreach (var buildingConfiguration in this.buildingConfigurations)
			{
				if (!System.Array.Exists(scenesInBuildNames, sceneName => sceneName.Equals(buildingConfiguration.popupSceneName)))
				{
					testFails.Add(string.Format("Building configuration '{0}' is referring to scene '{1}' which is not in the build settings!", buildingConfiguration.name, buildingConfiguration.popupSceneName));
				}
			}
			
			Assert.IsTrue(testFails.Count == 0, string.Join("\n", testFails.ToArray()));
		}
	}
}