using Configuration.Building;

namespace Buildings
{
    public interface IBuildingService
    {
        void PlaceBuilding(Building building);
        bool CanPlaceBuilding(Building building);
        bool CanBuildBuilding(BuildingConfiguration buildingConfig);

        void Save();
        void Load();
    }
}