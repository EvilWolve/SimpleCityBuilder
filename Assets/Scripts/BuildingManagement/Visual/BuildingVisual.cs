using UnityEngine;

namespace BuildingManagement.Visual
{
	public class BuildingVisual : MonoBehaviour, IBuildingVisual
	{
		Building building;
		
		public void SetBuilding(Building building)
		{
			this.UnregisterEvents();
			
			this.building = building;
			
			this.RegisterEvents();
		}

		public void ShowValidPlacement(bool isValid)
		{
			// TODO: Set mesh renderer's material tint to default color if valid or red otherwise.
			
			throw new System.NotImplementedException();
		}

		void RegisterEvents()
		{
			if (this.building != null)
			{
				this.building.onGridPositionChanged += this.OnGridPositionUpdated;
			}
		}

		void UnregisterEvents()
		{
			if (this.building != null)
			{
				this.building.onGridPositionChanged -= this.OnGridPositionUpdated;
			}
		}

		void OnGridPositionUpdated(Vector2Int gridPosition)
		{
			this.transform.position = new Vector3(gridPosition.x, 0f, gridPosition.y);
		}
	}
}