using Buildings;
using UnityEngine;
using Utilities;

namespace Popups.Settings
{
    public class SettingsPopup : BasePopup
    {
        protected override string SceneName
        {
            get { return "SettingsPopup"; }
        }

        public void ResetAndQuit()
        {
            IBuildingService buildingService = ServiceLocator.Instance.GetService<IBuildingService>();
            buildingService.Clear();
            buildingService.Save();
            
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}