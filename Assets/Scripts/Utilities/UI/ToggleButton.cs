using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Utilities.UI
{
    // DataObject has to be serialisable to be visible in the editor!
    [RequireComponent(typeof(Button))]
    public class ToggleButton<TDataObject> : MonoBehaviour
    {
        [SerializeField] Button button;
        [SerializeField] Animator animator;
        
        [SerializeField] TDataObject data;

        bool isSelected;

        public void SetSelected(bool selected)
        {
            if (this.isSelected != selected)
            {
                this.isSelected = selected;
                
                this.animator.SetBool("Selected", this.isSelected);
            }
        }

        public void AddClickAction(UnityAction clickAction)
        {
            this.button.onClick.AddListener(clickAction);
        }

        public TDataObject GetData()
        {
            return this.data;
        }
    }
}