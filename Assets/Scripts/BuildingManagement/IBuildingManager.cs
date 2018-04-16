using Configuration.Building;

namespace BuildingManagement
{
    public interface IBuildingManager
    {
        void PlaceBuilding(Building building);
        bool CanPlaceBuilding(Building building);
        bool CanBuildBuilding(BuildingConfiguration building);

        void Save();
        void Load();
    }
}