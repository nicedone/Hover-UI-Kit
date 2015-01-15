﻿using System;

namespace Henu.Navigation {

	/*================================================================================================*/
	public class NavItemSlider : NavItem {

		public int? Steps { get; set; }
		public Func<float, string> ValueToLabel { get; set; }

		private float vCurrValue;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public NavItemSlider(string pLabel) : base(ItemType.Slider, pLabel) {
			ValueToLabel = (v => 
				(string.IsNullOrEmpty(Label) ? "" : Label+": ")+(v*100).ToString("0.0")+"%"
			);
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public float CurrentValue {
			get {
				return vCurrValue;
			}
			set {
				vCurrValue = Math.Max(0, Math.Min(1, value));
			}
		}

	}

}