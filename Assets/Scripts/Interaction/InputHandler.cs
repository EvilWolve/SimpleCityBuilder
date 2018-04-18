using Buildings.Visual;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interaction
{
    public class InputHandler : MonoBehaviour
    {
        bool canHandleInput;

        int buildingLayerMask;
        
        void Awake()
        {
            this.canHandleInput = true;

            this.buildingLayerMask = 1 << LayerMask.NameToLayer("Building");
            
            this.RegisterEvents();
        }

        void OnDestroy()
        {
            this.UnregisterEvents();
        }

        void RegisterEvents()
        {
            BuildingMover.onMovementStarted += this.OnBuildingMoveStarted;
            BuildingMover.onMovementEnded += this.OnBuildingMoveEnded;
        }

        void UnregisterEvents()
        {
            BuildingMover.onMovementStarted -= this.OnBuildingMoveStarted;
            BuildingMover.onMovementEnded -= this.OnBuildingMoveEnded;
        }

        void OnBuildingMoveStarted()
        {
            this.canHandleInput = false;
        }

        void OnBuildingMoveEnded()
        {
            this.canHandleInput = true;
        }

        void Update()
        {
            if (!this.canHandleInput)
                return;

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, this.buildingLayerMask))
                {
                    BuildingVisual buildingVisual = hit.collider.GetComponent<BuildingVisual>();
                    if (buildingVisual != null)
                    {
                        buildingVisual.SpawnPopup();
                    }
                }
            }
        }
    }
}