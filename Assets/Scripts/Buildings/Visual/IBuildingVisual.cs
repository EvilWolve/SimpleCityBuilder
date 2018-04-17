namespace Buildings.Visual
{
	public interface IBuildingVisual
	{
		void SetVisible(bool visible);
		void ShowValidPlacement(bool isValid);
		void Remove();
	}
}