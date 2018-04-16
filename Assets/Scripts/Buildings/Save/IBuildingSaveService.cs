using System.Collections.Generic;

namespace Buildings.Save
{
    public interface IBuildingSaveService
    {
        void SaveBuildings(List<Building> buildings);
        List<Building> LoadBuildings();
    }
}