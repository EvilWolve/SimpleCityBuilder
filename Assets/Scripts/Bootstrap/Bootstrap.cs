using Buildings;
using Buildings.Visual;
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
            
            IBuildingService buildingService = ServiceLocator.Instance.GetService<IBuildingService>();
            buildingService.Load();
            
            IBuildingConfigurationService buildingConfigurationService = ServiceLocator.Instance.GetService<IBuildingConfigurationService>();
            BuildingLibrary library = Resources.Load<BuildingLibrary>("Building Library");
            buildingConfigurationService.UpdateConfiguration(library);
        }

        void InitialiseServices()
        {
            ServiceLocator.Instance.ProvideService<IBuildingConfigurationService>(new BuildingConfigurationService());
            ServiceLocator.Instance.ProvideService<IBuildingService>(new BuildingService());
            ServiceLocator.Instance.ProvideService<IBuildingVisualFactory>(new BuildingVisualFactory());
        }

        void OnApplicationQuit()
        {
            IBuildingService buildingService = ServiceLocator.Instance.GetService<IBuildingService>();
            buildingService.Save();
        }
    }
}