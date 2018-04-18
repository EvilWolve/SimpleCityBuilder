using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Utilities.UI
{
	public class ToggleButtonGroup<TDataObject, TToggleButton> : MonoBehaviour where TToggleButton : ToggleButton<TDataObject>
	{
		[SerializeField] TToggleButton[] toggleButtons;
		
		[SerializeField] int defaultSelection = -1;

		public delegate void OnSelectionChangedDelegate();
		public event OnSelectionChangedDelegate onSelectionChanged;

		public TDataObject CurrentData
		{
			get
			{
				this.AssertValidIndex(this.CurrentSelection);

				return this.toggleButtons[this.CurrentSelection].GetData();
			}
		}

		int currentSelection = -1;
		int CurrentSelection
		{
			get { return this.currentSelection; }
			set
			{
				this.AssertValidIndex(value);
				
				if (this.currentSelection == value)
					return;

				if (this.currentSelection >= 0 && this.currentSelection < this.toggleButtons.Length)
				{
					this.toggleButtons[this.currentSelection].SetSelected(false);
				}

				this.currentSelection = value;
				
				this.toggleButtons[this.currentSelection].SetSelected(true);

				if (this.onSelectionChanged != null)
				{
					this.onSelectionChanged();
				}
			}
		}

		public void Init()
		{
			this.HandleDefaultSelection();
			
			this.SetOnClickActions();
		}

		void HandleDefaultSelection()
		{
			if (this.defaultSelection < 0)
			{
				this.CurrentSelection = 0;
			}
			else
			{
				this.CurrentSelection = this.defaultSelection;
			}
		}

		void SetOnClickActions()
		{
			for (int i = 0; i < this.toggleButtons.Length; i++)
			{
				this.toggleButtons[i].AddClickAction(this.CreateOnClickAction(i));
			}
		}

		UnityAction CreateOnClickAction(int index)
		{
			return () => this.CurrentSelection = index;
		}

		void AssertValidIndex(int index)
		{
			Assert.IsTrue(index >= 0 && index < this.toggleButtons.Length);
		}
	}
}