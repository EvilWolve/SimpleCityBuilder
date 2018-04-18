using Popups;
using Popups.Buildings;
using UnityEngine;
using Utilities;

namespace Buildings.Visual
{
	public class BuildingVisual : MonoBehaviour, IBuildingVisual
	{
		[SerializeField] BoxCollider boxCollider;

		[SerializeField] Transform visualRoot;
		
		MeshRenderer meshRenderer;
		
		Building building;
		
		public void SetBuilding(Building building)
		{
			this.UnregisterEvents();
			
			this.building = building;
			
			this.boxCollider.center = new Vector3(building.GridArea.Center.x, this.boxCollider.center.y, building.GridArea.Center.y);
			this.boxCollider.size = new Vector3(this.building.GridArea.Width, this.boxCollider.size.y, this.building.GridArea.Height);

			this.visualRoot.position = new Vector3(building.GridArea.Center.x, 0f, building.GridArea.Center.y);
			this.visualRoot.localScale = new Vector3(this.building.GridArea.Width, 1f, this.building.GridArea.Height);

			GameObject meshPrefab = this.building.Config.prefab;
			GameObject meshObj = Object.Instantiate(meshPrefab, this.visualRoot);
			meshObj.layer = this.gameObject.layer;
			
			this.meshRenderer = meshObj.GetComponentInChildren<MeshRenderer>();
			this.meshRenderer.material.SetColor("_Color", this.building.Config.mainColor);
			
			this.RegisterEvents();
		}

		public void SetVisible(bool visible)
		{
			if (this.gameObject.activeSelf != visible)
			{
				this.gameObject.SetActive(visible);
			}
		}

		public void ShowValidPlacement(bool isValid)
		{
			this.meshRenderer.material.SetColor("_Color", isValid ? this.building.Config.mainColor : Color.red);
		}

		public void Remove()
		{
			Object.Destroy(this.gameObject);
		}

		void RegisterEvents()
		{
			if (this.building != null)
			{
				this.building.onGridPositionChanged += this.OnGridPositionUpdated;
			}
		}

		void UnregisterEvents()
		{
			if (this.building != null)
			{
				this.building.onGridPositionChanged -= this.OnGridPositionUpdated;
			}
		}

		void OnGridPositionUpdated(Vector2Int gridPosition)
		{
			this.transform.position = new Vector3(gridPosition.x, 0f, gridPosition.y);
		}

		public void SpawnPopup()
		{
			IPopupSpawner popupSpawner = ServiceLocator.Instance.GetService<IPopupSpawner>();
			popupSpawner.RequestSpawn(this.building.Config.popupSceneName, new BuildingPopup.BuildingSpawnInfo()
			{
				config = this.building.Config
			});
		}
	}
}