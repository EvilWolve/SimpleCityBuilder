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

        BuildingConfiguration config;

        public void Init(BuildingConfiguration config)
        {
            this.config = config;

            this.title.text = this.config.buildingName;
            this.icon.sprite = this.config.icon;
        }
    }
}
