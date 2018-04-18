using Board;
using Buildings;
using Buildings.Visual;
using Configuration.Building;
using UnityEngine;
using Utilities;

namespace Interaction
{
    public class BuildingMover
    {
        public delegate void OnMovementStartedDelegate();
        public static event OnMovementStartedDelegate onMovementStarted;
        
        public delegate void OnMovementEndedDelegate();
        public static event OnMovementEndedDelegate onMovementEnded;
        
        Building building;
        IBuildingVisual visual;

        IGameboard gameboard;

        public void BeginMoving()
        {
            if (BuildingMover.onMovementStarted != null)
            {
                BuildingMover.onMovementStarted();
            }
        }

        public void EndMoving()
        {
            this.building = null;
            this.visual = null;
            
            if (BuildingMover.onMovementEnded != null)
            {
                BuildingMover.onMovementEnded();
            }
        }
        
        public void InitWithConfig(BuildingConfiguration config)
        {
            this.Init(new Building(config));
        }
        
        public void InitWithBuilding(Building building)
        {
            // TODO: Unregister building from buildingService and gameboard?
            
            this.Init(building);
        }

        void Init(Building building)
        {
            this.building = building;

            this.gameboard = ServiceLocator.Instance.GetService<IGameboard>();
            
            this.CreateVisual();
        }

        void CreateVisual()
        {
            IBuildingVisualFactory visualFactory = ServiceLocator.Instance.GetService<IBuildingVisualFactory>();
            this.visual = visualFactory.CreateVisualForBuilding(this.building);
            
            this.SetVisible(false);
        }

        public void SetVisible(bool visible)
        {
            this.visual.SetVisible(visible);
        }

        public void UpdatePosition(Vector3 targetPosition)
        {
            Vector2 adjustedPosition = new Vector2(targetPosition.x - this.building.GridArea.Width / 2f, targetPosition.z - this.building.GridArea.Height / 2f);

            int x = Mathf.RoundToInt(adjustedPosition.x);
            int y = Mathf.RoundToInt(adjustedPosition.y);
            Vector2Int gridPosition = new Vector2Int(x, y);
            
            this.EvaluateGridPosition(gridPosition);
        }

        void EvaluateGridPosition(Vector2Int gridPosition)
        {
            this.building.SetGridPosition(gridPosition);
            
            bool isValidPlacement = this.gameboard.IsValidPlacement(this.building.GridArea);
            this.visual.ShowValidPlacement(isValidPlacement);
        }

        public void SetHasMovedOffGrid()
        {
            this.EvaluateGridPosition(new Vector2Int(-1, -1));
            this.visual.SetVisible(false);
        }

        public void TryToPlace()
        {
            IBuildingService buildingService = ServiceLocator.Instance.GetService<IBuildingService>();

            if (buildingService.CanPlaceBuilding(this.building))
            {
                buildingService.PlaceBuilding(this.building);
            }
            else
            {
                this.visual.Remove();
            }
        }
    }
}