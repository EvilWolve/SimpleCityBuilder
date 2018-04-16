namespace BuildingManagement.Visual
{
	public interface IBuildingVisualFactory
	{
		IBuildingVisual CreateVisualForBuilding(Building building);
	}
}