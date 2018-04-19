using UnityEngine;

namespace UI
{
    public class SlideInPanel : MonoBehaviour
    {
        [SerializeField] Animator slideInAnimator;

        bool isVisible = true;

        void Awake()
        {
            this.isVisible = true;
            this.RefreshAnimations();
        }

        public void ToggleVisibility()
        {
            this.isVisible = !this.isVisible;
            
            this.RefreshAnimations();
        }

        void RefreshAnimations()
        {
            this.slideInAnimator.SetBool("IsVisible", this.isVisible);
        }
    }
}