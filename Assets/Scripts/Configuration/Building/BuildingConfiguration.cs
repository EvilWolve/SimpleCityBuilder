using UnityEngine;

namespace Configuration.Building
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Building Configuration", menuName = "Building/Building Configuration")]
    public class BuildingConfiguration : ScriptableObject
    {
        public string buildingName;

        public BuildingType buildingType;
        public int maxAmount;

        public Vector2Int dimensions;

        public Sprite icon;
        public GameObject prefab;
        public Color mainColor;
    }
}
