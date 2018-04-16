namespace Buildings.Visual
{
	public class DummyBuildingVisualFactory : IBuildingVisualFactory
	{
		public IBuildingVisual CreateVisualForBuilding(Building building)
		{
			DummyBuildingVisual visual = new DummyBuildingVisual();
			visual.SetBuilding(building);

			return visual;
		}
	}
}