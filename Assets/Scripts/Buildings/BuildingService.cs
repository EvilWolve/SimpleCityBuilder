using System.Collections.Generic;
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
         
         //TODO: Reference to GameBoard here

         List<Building> buildings = new List<Building>();
         
         public BuildingService()
         {
             this.saveService = new BuildingSaveService();
         }
         
         public void PlaceBuilding(Building building)
         {
             // TODO: Building will already have the correct location, we just need to then communicate that to the GameBoard here
             
             this.buildings.Add(building);
         }

         public bool CanPlaceBuilding(Building building)
         {
             // TODO: The building has the target location, so we can query the GameBoard from here to check if a slot with the building's location and dimensions is occupied.
             
             throw new System.NotImplementedException();
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