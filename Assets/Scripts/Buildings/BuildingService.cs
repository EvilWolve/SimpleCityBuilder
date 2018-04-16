using System.Collections.Generic;
using Board;
using Buildings.Save;
using Configuration.Building;

namespace Buildings
{
     // This will likely be used by:
     // The building shop display to check if a building can be built
     // The building placement logic to check if the spot is valid and to finalise placement
     // The save/load calls at startup and shutdown
     public class BuildingService : IBuildingService
     {
         readonly IBuildingSaveService saveService;

         readonly IGameboard gameboard;

         List<Building> buildings = new List<Building>();
         
         public BuildingService()
         {
             this.saveService = new BuildingSaveService();
             
             this.gameboard = new Gameboard();
         }
         
         public void PlaceBuilding(Building building)
         {
             this.gameboard.SetOccupied(building.GridArea, true);
             
             this.buildings.Add(building);
         }

         public bool CanPlaceBuilding(Building building)
         {
             return this.gameboard.IsOccupied(building.GridArea);
         }

         public bool CanBuildBuilding(BuildingConfiguration buildingConfig)
         {
             if (buildingConfig.maxAmount < 0)
                 return true;
             
             int amount = 0;
             foreach (var building in this.buildings)
             {
                 if (building.Config.Equals(buildingConfig))
                 {
                     amount++;
                 }
             }

             return amount < buildingConfig.maxAmount;
         }

         public void Save()
         {
             this.saveService.SaveBuildings(this.buildings);
         }

         public void Load()
         {
             this.buildings = this.saveService.LoadBuildings();
         }
     }
 }