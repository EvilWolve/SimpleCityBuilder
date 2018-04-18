using UnityEngine;
using Utilities;

namespace Popups
{
    public abstract class BasePopup : MonoBehaviour
    {
        public abstract class SpawnInfo { }

        protected abstract string SceneName { get; }

        void Awake()
        {
            IPopupSpawner spawner = ServiceLocator.Instance.GetService<IPopupSpawner>();
            SpawnInfo info = spawner.HandlePopupSpawned(this.SceneName);

            this.InitContent(info);
        }

        #region Abstract methods
        
        protected abstract void InitContent(SpawnInfo info);
        protected abstract void Close();

        #endregion
    }
}