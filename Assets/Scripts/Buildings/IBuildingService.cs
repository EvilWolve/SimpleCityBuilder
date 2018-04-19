using System.Collections.Generic;
using Configuration.Building;

namespace Buildings
{
    public delegate void OnBuildingsUpdatedDelegate();
    
    public interface IBuildingService
    {
        void PlaceBuilding(Building building);
        bool CanPlaceBuilding(Building building);
        bool CanBuildBuilding(BuildingConfiguration buildingConfig);

        void Clear();
        void Save();
        void Load();
        
        List<Building> GetAllBuildings();

        void RegisterBuildingUpdate(OnBuildingsUpdatedDelegate updateDelegate);
        void UnregisterBuildingUpdate(OnBuildingsUpdatedDelegate updateDelegate);
    }
}