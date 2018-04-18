using UnityEngine;

namespace Configuration.Building
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Building Configuration", menuName = "Building/Building Configuration")]
    public class BuildingConfiguration : ScriptableObject
    {
        public string buildingName;

        public BuildingType buildingType;
        public int maxAmount; // -1 for infinite

        public Vector2Int dimensions;

        public Sprite icon;
        public GameObject prototype;
        public GameObject prefab;
        
        public Color mainColor;
        public Color secondaryColor;

        public string popupSceneName;

        public override bool Equals(object obj)
        {
            BuildingConfiguration other = obj as BuildingConfiguration;

            if (other == null)
                return false;

            return this.name.Equals(other.name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
