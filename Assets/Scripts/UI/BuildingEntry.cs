using Configuration.Building;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace UI
{
    public class BuildingEntry : MonoBehaviour
    {
        [SerializeField] Image background;
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] Image icon;
        [SerializeField] GameObject locked;

        [SerializeField] DraggableBuilding draggable;

        public void Init(BuildingConfiguration config, bool canBuild)
        {
            this.background.color = config.mainColor;
            this.title.text = config.buildingName;
            this.icon.sprite = config.icon;
            
            this.locked.SetActive(!canBuild);

            this.draggable.Init(config);
            this.draggable.enabled = canBuild;
        }
    }
}
