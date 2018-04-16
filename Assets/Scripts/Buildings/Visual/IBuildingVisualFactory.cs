namespace Buildings.Visual
{
	public interface IBuildingVisualFactory
	{
		IBuildingVisual CreateVisualForBuilding(Building building);
	}
}