using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Popups
{
    using SpawnInfo = BasePopup.SpawnInfo;

    public interface IPopupSpawner
    {
        void RequestSpawn(string sceneName, SpawnInfo info);
        
        SpawnInfo HandlePopupSpawned(string sceneName);
    }
    
    public class PopupSpawner : IPopupSpawner
    {
        Dictionary<string, SpawnInfo> popupsToSpawn = new Dictionary<string, SpawnInfo>();

        public void RequestSpawn(string sceneName, SpawnInfo info)
        {
            if (BasePopup.IsPopupVisible(sceneName))
                return;
            
            if (this.popupsToSpawn.ContainsKey(sceneName))
            {
                this.popupsToSpawn[sceneName] = info;
            }
            else
            {
                this.popupsToSpawn.Add(sceneName, info);

                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }

        public SpawnInfo HandlePopupSpawned(string sceneName)
        {
            SpawnInfo info = this.popupsToSpawn[sceneName];
            this.popupsToSpawn.Remove(sceneName);

            return info;
        }
    }
}