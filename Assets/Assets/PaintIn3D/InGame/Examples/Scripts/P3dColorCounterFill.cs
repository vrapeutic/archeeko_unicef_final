﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace PaintIn3D.Examples
{
	/// <summary>This component fills the attached UI Image based on the total amount of pixels that have been painted in the specified <b>P3dColorCounter</b> components.</summary>
	[RequireComponent(typeof(Image))]
	[HelpURL(P3dHelper.HelpUrlPrefix + "P3dColorCounterFill")]
	[AddComponentMenu(P3dHelper.ComponentMenuPrefix + "Examples/Color Counter Fill")]
	public class P3dColorCounterFill : MonoBehaviour
	{
		/// <summary>This allows you to specify the counters that will be used.
		/// Zero = All active and enabled counters in the scene.</summary>
		public List<P3dColorCounter> Counters { get { if (counters == null) counters = new List<P3dColorCounter>(); return counters; } } [SerializeField] private List<P3dColorCounter> counters;

		/// <summary>This allows you to set which color will be handled by this component.</summary>
		public P3dColor Color { set { color = value; } get { return color; } } [SerializeField] private P3dColor color;

		/// <summary>Inverse the fill?</summary>
		public bool Inverse { set { inverse = value; } get { return inverse; } } [SerializeField] private bool inverse;

		[System.NonSerialized]
		private Image cachedImage;

		protected virtual void OnEnable()
		{
			cachedImage = GetComponent<Image>();
		}

		protected virtual void Update()
		{
			var finalCounters = counters.Count > 0 ? counters : null;
			var ratio         = P3dColorCounter.GetRatio(color, finalCounters);

			if (inverse == true)
			{
				ratio = 1.0f - ratio;
			}

			cachedImage.fillAmount = Mathf.Clamp01(ratio);
		}
	}
}

#if UNITY_EDITOR
namespace PaintIn3D.Examples
{
	using UnityEditor;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(P3dColorCounterFill))]
	public class P3dColorCounterFill_Editor : P3dEditor<P3dColorCounterFill>
	{
		protected override void OnInspector()
		{
			Draw("counters", "This allows you to specify the counters that will be used.\n\nZero = All active and enabled counters in the scene.");

			Separator();

			BeginError(Any(t => t.Color == null));
				Draw("color", "This allows you to set which color will be handled by this component.");
			EndError();
			Draw("inverse", "Inverse the fill?");
		}
	}
}
#endif