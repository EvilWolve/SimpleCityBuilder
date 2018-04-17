using UnityEngine;
using UnityEngine.Assertions;

namespace Buildings.Visual
{
	public class BuildingVisualFactory : IBuildingVisualFactory
	{
		Transform buildingRoot;

		Transform GetOrCreateRoot()
		{
			if (this.buildingRoot == null)
			{
				GameObject go = new GameObject("Building Root");
				this.buildingRoot = go.transform;
			}

			return this.buildingRoot;
		}
		
		public IBuildingVisual CreateVisualForBuilding(Building building)
		{
			GameObject prefab = building.Config.prototype;
			Assert.IsNotNull(prefab);

			GameObject go = Object.Instantiate(prefab, this.GetOrCreateRoot());
			BuildingVisual visual = go.GetComponent<BuildingVisual>();
			
			visual.SetBuilding(building);

			return visual;
		}
	}
}