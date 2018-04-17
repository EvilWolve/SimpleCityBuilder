using System.Collections.Generic;
using Configuration.Building;
using UnityEngine;
using Utilities;

namespace UI
{
	public class BuildingMenu : MonoBehaviour
	{
		[SerializeField] Transform gridRoot;
		[SerializeField] GameObject entryPrefab;

		IBuildingConfigurationService buildingConfigurationService;

		readonly List<BuildingEntry> entries = new List<BuildingEntry>();

		BuildingType selectedBuildingType;
		
		// TODO: Add functionality to tabs to switch building types

		void Awake()
		{
			this.buildingConfigurationService = ServiceLocator.Instance.GetService<IBuildingConfigurationService>();
			
			this.selectedBuildingType = BuildingType.COMMON;
			
			this.InitialiseBuildings();
		}

		void InitialiseBuildings()
		{
			List<BuildingConfiguration> buildingsOfType = this.buildingConfigurationService.GetBuildingsOfType(this.selectedBuildingType);

			for (int i = 0; i < buildingsOfType.Count; i++)
			{
				BuildingEntry entry;
				if (i < this.entries.Count)
				{
					entry = this.entries[i];
				}
				else
				{
					GameObject entryObj = Object.Instantiate(this.entryPrefab, this.gridRoot);
					entry = entryObj.GetComponent<BuildingEntry>();
					
					this.entries.Add(entry);
				}
				
				entry.gameObject.SetActive(true);
				entry.Init(buildingsOfType[i]);
			}

			for (int i = buildingsOfType.Count; i < this.entries.Count; i++)
			{
				this.entries[i].gameObject.SetActive(false);
			}
		}
	}
}