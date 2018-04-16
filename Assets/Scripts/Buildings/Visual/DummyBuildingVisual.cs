using UnityEngine;

namespace Buildings.Visual
{
	public class DummyBuildingVisual : IBuildingVisual
	{
		public void SetBuilding(Building building) { Debug.Log(string.Format("Setting building {0}", building.Config.name));}

		public void ShowValidPlacement(bool isValid) { Debug.Log(string.Format("Setting building visual's placement to {0}", isValid));}
	}
}