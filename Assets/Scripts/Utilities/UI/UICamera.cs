using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI
{
    public class UICamera : MonoBehaviour
    {
        public static UICamera Current;

        void Awake()
        {
            if (UICamera.Current != null)
            {
                Object.Destroy(this.gameObject);
                return;
            }

            UICamera.Current = this;
        }
    }
}