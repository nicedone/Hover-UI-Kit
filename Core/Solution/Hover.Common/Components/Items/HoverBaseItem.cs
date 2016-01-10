﻿using System.Collections.Generic;
using Hover.Common.Items;
using Hover.Common.Util;
using UnityEngine;

namespace Hover.Common.Components.Items {

	/*================================================================================================*/
	public abstract class HoverBaseItem : MonoBehaviour {

		public IBaseItem Item { get; private set; }

		private int AutoId;
		public string Id;
		public string Label;
		public float Width;
		public float Height;

		public bool IsEnabled;
		public bool IsVisible;
		private bool IsAncestryEnabled;
		private bool IsAncestryVisible;

		private readonly ValueBinder<string> vBindId;
		private readonly ValueBinder<string> vBindLabel;
		private readonly ValueBinder<float> vBindWidth;
		private readonly ValueBinder<float> vBindHeight;

		private readonly ValueBinder<bool> vBindEnabled;
		private readonly ValueBinder<bool> vBindVisible;

		private BaseItem vFullItem;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		protected HoverBaseItem() {
			vBindId = new ValueBinder<string>(
				(x => { Item.Id = x; }),
				(x => { Id = x; }),
				ValueBinder.AreStringsEqual
			);

			vBindLabel = new ValueBinder<string>(
				(x => { Item.Label = x; }),
				(x => { Label = x; }),
				ValueBinder.AreStringsEqual
			);

			vBindWidth = new ValueBinder<float>(
				(x => { vFullItem.Width = x; }),
				(x => { Width = x; }),
				ValueBinder.AreFloatsEqual
			);

			vBindHeight = new ValueBinder<float>(
				(x => { vFullItem.Height = x; }),
				(x => { Height = x; }),
				ValueBinder.AreFloatsEqual
			);

			vBindEnabled = new ValueBinder<bool>(
				(x => { Item.IsEnabled = x; }),
				(x => { IsEnabled = x; }),
				ValueBinder.AreBoolsEqual
			);

			vBindVisible = new ValueBinder<bool>(
				(x => { Item.IsVisible = x; }),
				(x => { IsVisible = x; }),
				ValueBinder.AreBoolsEqual
			);
		}

		/*--------------------------------------------------------------------------------------------*/
		protected void Init(BaseItem pItem) {
			Item = pItem;
			vFullItem = pItem;
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public virtual void Awake() {
			vFullItem.DisplayContainer = gameObject;
		}

		/*--------------------------------------------------------------------------------------------*/
		public virtual void Start() {
			UpdateAllValues(true);
		}

		/*--------------------------------------------------------------------------------------------*/
		public virtual void Update() {
			UpdateAllValues();
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		protected virtual void UpdateAllValues(bool pForceUpdate=false) {
			AutoId = Item.AutoId;
			vBindId.UpdateValuesIfChanged(Item.Id, Id, pForceUpdate);
			vBindLabel.UpdateValuesIfChanged(Item.Label, Label, pForceUpdate);
			vBindWidth.UpdateValuesIfChanged(Item.Width, Width, pForceUpdate);
			vBindHeight.UpdateValuesIfChanged(Item.Height, Height, pForceUpdate);

			vBindEnabled.UpdateValuesIfChanged(Item.IsEnabled, IsEnabled, pForceUpdate);
			vBindVisible.UpdateValuesIfChanged(Item.IsVisible, IsVisible, pForceUpdate);
			IsAncestryEnabled = Item.IsAncestryEnabled;
			IsAncestryVisible = Item.IsAncestryVisible;
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public static IBaseItem[] GetChildItems(GameObject pParentGo) {
			Transform tx = pParentGo.transform;
			int childCount = tx.childCount;
			var items = new List<IBaseItem>();

			for ( int i = 0 ; i < childCount ; ++i ) {
				HoverBaseItem hni = tx.GetChild(i).GetComponent<HoverBaseItem>();
				IBaseItem item = hni.Item;

				if ( !item.IsVisible ) {
					continue;
				}

				items.Add(item);
			}

			return items.ToArray();
		}

	}

}
