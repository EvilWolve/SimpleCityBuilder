using System;
using UnityEngine;

namespace BuildingManagement.Save
{
	[Serializable]
	public class BuildingSaveData
	{
		public string configName;
		public Vector2Int gridPosition;
		
		// Add other data such as building level here later on
	}
}