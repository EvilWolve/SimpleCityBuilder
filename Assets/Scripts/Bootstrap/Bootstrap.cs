using BuildingManagement;
using BuildingManagement.Visual;
using Configuration.Building;
using UnityEngine;
using Utilities;

namespace Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        void Awake()
        {
            this.InitialiseServices();
            
            IBuildingManager buildingManager = ServiceLocator.Instance.GetService<IBuildingManager>();
            buildingManager.Load();
            
            IBuildingConfigurationService buildingConfigurationService = ServiceLocator.Instance.GetService<IBuildingConfigurationService>();
            BuildingLibrary library = Resources.Load<BuildingLibrary>("Building Library");
            buildingConfigurationService.UpdateConfiguration(library);
        }

        void InitialiseServices()
        {
            ServiceLocator.Instance.ProvideService<IBuildingConfigurationService>(new BuildingConfigurationService());
            ServiceLocator.Instance.ProvideService<IBuildingManager>(new BuildingManager());
            ServiceLocator.Instance.ProvideService<IBuildingVisualFactory>(new BuildingVisualFactory());
        }

        void OnApplicationQuit()
        {
            IBuildingManager buildingManager = ServiceLocator.Instance.GetService<IBuildingManager>();
            buildingManager.Save();
        }
    }
}