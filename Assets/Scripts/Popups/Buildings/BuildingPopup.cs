using Configuration.Building;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Popups.Buildings
{
    public abstract class BuildingPopup : BasePopup
    {
        [SerializeField] TextMeshProUGUI titleText;
        
        [SerializeField] Image buildingIcon;
        
        [SerializeField] Image header;
        [SerializeField] Image info;
        
        [SerializeField] Image background;
        
        public class BuildingSpawnInfo : SpawnInfo
        {
            public BuildingConfiguration config;
        }

        BuildingConfiguration config;
        
        protected override void InitContent(SpawnInfo info)
        {
            BuildingSpawnInfo buildingSpawnInfo = info as BuildingSpawnInfo;
            
            Assert.IsNotNull(buildingSpawnInfo, "Tried to spawn building popup without building spawn info!");

            this.config = buildingSpawnInfo.config;
            
            this.InitSharedVisuals();
        }

        void InitSharedVisuals()
        {
            this.titleText.text = this.config.buildingName;

            this.buildingIcon.sprite = this.config.icon;
            
            this.header.color = this.config.mainColor;
            this.info.color = this.config.mainColor;
            
            this.background.color = this.config.secondaryColor;
        }
    }
}