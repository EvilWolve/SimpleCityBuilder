using Board;
using Board.Visual;
using Buildings;
using Buildings.Visual;
using Configuration.Board;
using Configuration.Building;
using Popups;
using Utilities;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        void Awake()
        {
            Object.DontDestroyOnLoad(this.gameObject);
            
            this.RegisterServices();
            
            this.InitialiseServices();
            
            this.LoadLevelScene();
        }

        void RegisterServices()
        {
            ServiceLocator.Instance.ProvideService<IBuildingConfigurationService>(new BuildingConfigurationService());
            ServiceLocator.Instance.ProvideService<IBuildingVisualFactory>(new BuildingVisualFactory());
            ServiceLocator.Instance.ProvideService<IGameboardVisualFactory>(new GameboardVisualFactory());
            
            ServiceLocator.Instance.ProvideService<IPopupSpawner>(new PopupSpawner());
            
            ServiceLocator.Instance.ProvideService<IGameboard>(new Gameboard());
            
            // BuildingService depends on the previous ones, initialise them first!
            ServiceLocator.Instance.ProvideService<IBuildingService>(new BuildingService());
        }
        
        void InitialiseServices()
        {
            IBuildingConfigurationService buildingConfigurationService = ServiceLocator.Instance.GetService<IBuildingConfigurationService>();
            BuildingLibrary library = Resources.Load<BuildingLibrary>(BuildingLibrary.RESOURCE_LOCATION);
            buildingConfigurationService.UpdateConfiguration(library);

            IGameboard gameboard = ServiceLocator.Instance.GetService<IGameboard>();
            GameboardConfiguration gameboardConfig = Resources.Load<GameboardConfiguration>(GameboardConfiguration.RESOURCE_LOCATION);
            gameboard.Initialise(gameboardConfig);
            
            IBuildingService buildingService = ServiceLocator.Instance.GetService<IBuildingService>();
            buildingService.Load();
        }

        void LoadLevelScene()
        {
            SceneManager.LoadScene("Homebase");
        }

        void OnApplicationQuit()
        {
            IBuildingService buildingService = ServiceLocator.Instance.GetService<IBuildingService>();
            buildingService.Save();
        }
    }
}