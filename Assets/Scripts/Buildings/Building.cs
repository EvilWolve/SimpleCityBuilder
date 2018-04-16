using Board;
using Buildings.Save;
using Buildings.Visual;
using Configuration.Building;
using UnityEngine;
using Utilities;

namespace Buildings
{
    public class Building
    {
        public delegate void OnGridPositionChangedDelegate(Vector2Int gridPosition);
        public event OnGridPositionChangedDelegate onGridPositionChanged;
        
        public BuildingConfiguration Config { get; private set; }
        
        public IBuildingVisual Visual { get; private set; }

        Vector2Int gridPosition;
        
        public GridRect GridArea { get { return new GridRect(this.gridPosition, this.Config.dimensions); } }

        public Building(BuildingConfiguration config)
        {
            this.Config = config;

            IBuildingVisualFactory buildingVisualFactory = ServiceLocator.Instance.GetService<IBuildingVisualFactory>();
            this.Visual = buildingVisualFactory.CreateVisualForBuilding(this);
        }

        public void SetGridPosition(Vector2Int newGridPosition)
        {
            this.gridPosition = newGridPosition;

            if (this.onGridPositionChanged != null)
            {
                this.onGridPositionChanged(this.gridPosition);
            }
        }

        public BuildingSaveData CreateSaveData()
        {
            return new BuildingSaveData
            {
                configName = this.Config.name,
                gridPosition = this.gridPosition
            };
        }

        public override bool Equals(object obj)
        {
            Building other = obj as Building;

            if (other == null)
                return false;
            
            return this.Config.Equals(other.Config)
                   && this.gridPosition.Equals(other.gridPosition);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
