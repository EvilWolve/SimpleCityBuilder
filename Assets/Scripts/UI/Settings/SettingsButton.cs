using Popups;
using UnityEngine;
using Utilities;

namespace UI.Settings
{
    public class SettingsButton : MonoBehaviour
    {
        public void OpenSettings()
        {
            IPopupSpawner spawner = ServiceLocator.Instance.GetService<IPopupSpawner>();
            spawner.RequestSpawn("SettingsPopup", null);
        }
    }
}