﻿using System.Collections.Generic;
using Buildings;
using Configuration.Building;
using UnityEngine;
using Utilities;

namespace UI
{
	public class BuildingMenu : MonoBehaviour
	{
		[SerializeField] Transform gridRoot;
		[SerializeField] GameObject entryPrefab;

		IBuildingService buildingService;
		IBuildingConfigurationService buildingConfigurationService;

		readonly List<BuildingEntry> entries = new List<BuildingEntry>();

		BuildingType selectedBuildingType;
		
		// TODO: Add functionality to tabs to switch building types
		
		// TODO: Refresh menu when building layout has been modified, i.e. a building has been placed or removed

		void Awake()
		{
			this.buildingService = ServiceLocator.Instance.GetService<IBuildingService>();
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
				entry.Init(buildingsOfType[i], this.buildingService.CanBuildBuilding(buildingsOfType[i]));
			}

			for (int i = buildingsOfType.Count; i < this.entries.Count; i++)
			{
				this.entries[i].gameObject.SetActive(false);
			}
		}
	}
}