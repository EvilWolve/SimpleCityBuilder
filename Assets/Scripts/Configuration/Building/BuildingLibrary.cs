using UnityEngine;

namespace Configuration.Building
{
	[System.Serializable]
	[CreateAssetMenu(fileName = "New Building Library", menuName = "Building/Building Library")]
	public class BuildingLibrary : ScriptableObject
	{
		public const string RESOURCE_LOCATION = "Building Library";
		
		public BuildingConfiguration[] buildingConfigurations;
	}
}
