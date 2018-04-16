using UnityEngine;
using UnityEngine.Assertions;

namespace Buildings.Visual
{
	public class BuildingVisualFactory : IBuildingVisualFactory
	{
		public IBuildingVisual CreateVisualForBuilding(Building building)
		{
			GameObject prefab = building.Config.prefab;
			Assert.IsNotNull(prefab);

			GameObject go = Object.Instantiate(prefab);
			BuildingVisual visual = go.GetComponent<BuildingVisual>();

			return visual;
		}
	}
}