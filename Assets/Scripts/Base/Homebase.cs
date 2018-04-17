using Board;
using Board.Visual;
using Buildings;
using Buildings.Visual;
using UnityEngine;
using Utilities;

namespace Base
{
    public class Homebase : MonoBehaviour
    {
        void Awake()
        {
            this.CreateGameboard();
            
            this.CreateBuildings();
        }

        void CreateGameboard()
        {
            IGameboard gameboard = ServiceLocator.Instance.GetService<IGameboard>();
            IGameboardVisualFactory gameboardVisualFactory = ServiceLocator.Instance.GetService<IGameboardVisualFactory>();

            gameboardVisualFactory.CreateVisualForGameboard(gameboard);
        }

        void CreateBuildings()
        {
            IBuildingService buildingService = ServiceLocator.Instance.GetService<IBuildingService>();
            IBuildingVisualFactory buildingVisualFactory = ServiceLocator.Instance.GetService<IBuildingVisualFactory>();

            foreach (var building in buildingService.GetAllBuildings())
            {
                buildingVisualFactory.CreateVisualForBuilding(building);
            }
        }
    }
}