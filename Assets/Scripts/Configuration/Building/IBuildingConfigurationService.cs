using System.Collections.Generic;

namespace Configuration.Building
{
	public interface IBuildingConfigurationService
	{
		void UpdateConfiguration(BuildingLibrary library);
		
		BuildingConfiguration GetBuilding(string name);
		List<BuildingConfiguration> GetBuildingsOfType(BuildingType type);
	}
}