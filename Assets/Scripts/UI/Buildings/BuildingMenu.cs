using System.Collections.Generic;
using Buildings;
using Configuration.Building;
using UnityEngine;
using Utilities;

namespace UI.Buildings
{
	public class BuildingMenu : MonoBehaviour
	{
		[SerializeField] BuildingMenuToggleButtonGroup toggleButtonGroup;
		
		[SerializeField] Transform gridRoot;
		[SerializeField] GameObject entryPrefab;

		IBuildingService buildingService;
		IBuildingConfigurationService buildingConfigurationService;

		readonly List<BuildingEntry> entries = new List<BuildingEntry>();

		void Awake()
		{
			this.buildingService = ServiceLocator.Instance.GetService<IBuildingService>();
			this.buildingConfigurationService = ServiceLocator.Instance.GetService<IBuildingConfigurationService>();
			
			this.RegisterEvents();
			
			this.toggleButtonGroup.Init();
			
			this.InitialiseBuildings();
		}

		void OnDestroy()
		{
			this.UnregisterEvents();
		}

		void RegisterEvents()
		{
			this.buildingService.RegisterBuildingUpdate(this.OnBuildingsUpdated);
			
			this.toggleButtonGroup.onSelectionChanged += this.OnBuildingTypeSelectionChanged;
		}

		void UnregisterEvents()
		{
			this.buildingService.UnregisterBuildingUpdate(this.OnBuildingsUpdated);
			
			this.toggleButtonGroup.onSelectionChanged -= this.OnBuildingTypeSelectionChanged;
		}

		void OnBuildingsUpdated()
		{
			this.InitialiseBuildings();
		}

		void OnBuildingTypeSelectionChanged()
		{
			this.InitialiseBuildings();
		}

		void InitialiseBuildings()
		{
			List<BuildingConfiguration> buildingsOfType = this.buildingConfigurationService.GetBuildingsOfType(this.toggleButtonGroup.CurrentData);

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