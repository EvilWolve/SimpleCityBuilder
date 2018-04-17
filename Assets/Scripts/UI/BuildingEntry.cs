using Configuration.Building;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace UI
{
    public class BuildingEntry : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] Image icon;

        [SerializeField] DraggableBuilding draggable;

        public void Init(BuildingConfiguration config)
        {
            this.title.text = config.buildingName;
            this.icon.sprite = config.icon;

            this.draggable.Init(config);
        }
    }
}
