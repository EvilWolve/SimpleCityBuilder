using System.Collections.Generic;
using Configuration.Building;
using Save;
using Utilities;

namespace BuildingManagement.Save
{
	public class BuildingSaveService : IBuildingSaveService
	{
		readonly ISaveService saveService;

		const string BUILDUNG_SAVE_KEY = "buildings";

		public BuildingSaveService()
		{
			this.saveService = new PlayerPrefsSaveService();
		}

		public void SaveBuildings(List<Building> buildings)
		{
			BuildingSaveData[] saveData = new BuildingSaveData[buildings.Count];
			for (int i = 0; i < buildings.Count; i++)
			{
				saveData[i] = buildings[i].CreateSaveData();
			}
             
			FullBuildingSave fullSave = new FullBuildingSave
			{
				buildings = saveData
			};
             
			this.saveService.Save(BuildingSaveService.BUILDUNG_SAVE_KEY, fullSave);
		}

		public List<Building> LoadBuildings()
		{
			List<Building> result = new List<Building>();

			IBuildingConfigurationService buildingConfigurationService = ServiceLocator.Instance.GetService<IBuildingConfigurationService>();
			
			FullBuildingSave fullSave = this.saveService.Load<FullBuildingSave>(BuildingSaveService.BUILDUNG_SAVE_KEY);
			if (fullSave != null)
			{
				foreach (var saveData in fullSave.buildings)
				{
					BuildingConfiguration buildingConfig = buildingConfigurationService.GetBuilding(saveData.configName);
					Building building = new Building(buildingConfig);
					building.SetLocation(saveData.location);
					
					result.Add(building);
				}
			}

			return result;
		}
	}
}