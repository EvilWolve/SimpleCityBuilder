using BuildingManagement.Save;
using Configuration.Building;
using UnityEngine;

namespace BuildingManagement
{
    public class Building
    {
        BuildingConfiguration config;
        
        public Vector2Int Location { get; private set; }

        public Building(BuildingConfiguration config)
        {
            this.config = config;
        }

        public void SetLocation(Vector2Int newLocation)
        {
            this.Location = newLocation;
        }

        public BuildingSaveData CreateSaveData()
        {
            return new BuildingSaveData
            {
                configName = this.config.name,
                location = this.Location
            };
        }

        public override bool Equals(object obj)
        {
            Building other = obj as Building;
            return this.config.name.Equals(other.config.name)
                   && this.Location.Equals(other.Location);
        }
    }
}
