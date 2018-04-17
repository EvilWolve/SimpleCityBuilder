using System.Collections.Generic;
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
        
        // WARNING: This is for TESTING ONLY! Do NOT use this in regular gameplay. You should never need to know which buildings exist!
        List<Building> GetAllBuildings();
    }
}