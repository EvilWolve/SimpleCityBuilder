using Configuration.Building;
using Interaction;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Buildings
{
	public class DraggableBuilding : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		BuildingConfiguration config;

		BuildingMover mover;

		public void Init(BuildingConfiguration config)
		{
			this.config = config;
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			this.mover = new BuildingMover();
			this.mover.InitWithConfig(this.config);
			
			this.mover.BeginMoving();
		}

		public void OnDrag(PointerEventData eventData)
		{
			bool hitGrid = false;
			
			if (!EventSystem.current.IsPointerOverGameObject())
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Gameboard")))
				{
					hitGrid = true;
					
					this.mover.SetVisible(true);
					this.mover.UpdatePosition(hit.point);
				}
			}

			if (!hitGrid)
			{
				this.mover.SetHasMovedOffGrid();
			}
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			this.mover.TryToPlace();
			
			this.mover.EndMoving();
			this.mover = null;
		}
	}
}
