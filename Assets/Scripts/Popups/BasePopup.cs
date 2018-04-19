using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using Utilities;
using Utilities.UI;

namespace Popups
{
    public abstract class BasePopup : MonoBehaviour
    {
        [SerializeField] Canvas canvas;
        [SerializeField] Animator animator;
        
        public delegate void OnCloseDelegate();
        OnCloseDelegate onClose;
        
        public abstract class SpawnInfo
        {
            public OnCloseDelegate onClose;
        }

        protected abstract string SceneName { get; }

        static Dictionary<Type, BasePopup> visiblePopups = new Dictionary<Type, BasePopup>();

        void Awake()
        {
            IPopupSpawner spawner = ServiceLocator.Instance.GetService<IPopupSpawner>();
            SpawnInfo info = spawner.HandlePopupSpawned(this.SceneName);

            this.canvas.worldCamera = UICamera.Current.GetComponent<Camera>();
            
            this.RegisterPopup();

            this.InitContent(info);
            
            this.Appear();
        }

        void RegisterPopup()
        {
            BasePopup.visiblePopups.Add(this.GetType(), this);
        }

        protected virtual void InitContent(SpawnInfo info)
        {
            if (info != null)
            {
                this.onClose = info.onClose;
            }
        }

        void Appear()
        {
            if (this.animator != null)
            {
                this.animator.SetTrigger("Appear");
            }
        }

        void Disappear()
        {
            if (this.animator != null)
            {
                this.animator.SetTrigger("Disappear");
            }
            else
            {
                this.Close();
            }
        }

        public void OnCloseButtonPressed()
        {
            this.Disappear();
        }

        protected virtual void Close()
        {
            this.UnregisterPopup();
            
            if (this.onClose != null)
            {
                this.onClose();
            }
            
            this.gameObject.SetActive(false);

            SceneManager.UnloadSceneAsync(this.SceneName);
        }

        void UnregisterPopup()
        {
            Assert.IsTrue (BasePopup.visiblePopups.ContainsKey(this.GetType()), string.Format ("Popup {0} is being removed but is not in the list of visible popups!", this.SceneName));

            BasePopup.visiblePopups.Remove (this.GetType());
        }

        #region Animation events

        public void OnDisappearComplete()
        {
            this.Close();
        }

        #endregion

        #region Static access

        public static bool IsPopupVisible<T>() where T : BasePopup
        {
            return BasePopup.visiblePopups.ContainsKey(typeof (T));
        }

        public static bool IsPopupVisible(string sceneName)
        {
            foreach (var popup in BasePopup.visiblePopups.Values)
            {
                if (popup.SceneName.Equals(sceneName))
                    return true;
            }

            return false;
        }

        public static int GetVisiblePopupCount()
        {
            return BasePopup.visiblePopups.Count;
        }

        #endregion
    }
}