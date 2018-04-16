using System.Collections.Generic;

namespace BuildingManagement.Save
{
    public interface IBuildingSaveService
    {
        void SaveBuildings(List<Building> buildings);
        List<Building> LoadBuildings();
    }
}