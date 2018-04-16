namespace BuildingManagement.Visual
{
	public interface IBuildingVisual
	{
		void SetBuilding(Building building);
		void ShowValidPlacement(bool isValid);
	}
}