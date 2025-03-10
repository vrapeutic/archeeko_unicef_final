﻿using UnityEngine;
using UnityEngine.Events;

namespace PaintIn3D.Examples
{
	/// <summary>This component allows you to perform an event when the attached <b>P3dReadColor</b> component reads a specific color.</summary>
	[HelpURL(P3dHelper.HelpUrlPrefix + "P3dReadColorEvent")]
	[AddComponentMenu(P3dHelper.ComponentMenuPrefix + "Examples/Read Color Event")]
	[RequireComponent(typeof(P3dReadColor))]
	public class P3dReadColorEvent : MonoBehaviour
	{
		[System.Serializable] public class ColorEvent : UnityEvent<Color> {}

		/// <summary>This color we want to detect.</summary>
		public Color Color { set { color = value; } get { return color; } } [SerializeField] private Color color = Color.white;

		/// <summary>The RGBA values must be within this range of a color for it to be counted.</summary>
		public float Threshold { set { threshold = value; } get { return threshold; } } [Range(0.0f, 1.0f)] [SerializeField] private float threshold = 0.1f;

		/// <summary>When the expected color is read, this event will be invoked.
		/// Color = The expected color.</summary>
		public ColorEvent OnColor { get { if (onColor == null) onColor = new ColorEvent(); return onColor; } } [SerializeField] private ColorEvent onColor;

		[System.NonSerialized]
		private P3dReadColor cachedReadColor;

		protected virtual void OnEnable()
		{
			cachedReadColor = GetComponent<P3dReadColor>();

			cachedReadColor.OnColor.AddListener(HandleColor);
		}

		protected virtual void OnDisable()
		{
			cachedReadColor.OnColor.RemoveListener(HandleColor);
		}

		private void HandleColor(Color read)
		{
			var color32     = (Color32)color;
			var read32      = (Color32)read;
			var threshold32 = (int)(threshold * 255.0f);
			var distance    = 0;

			distance += System.Math.Abs(color32.r - read32.r);
			distance += System.Math.Abs(color32.g - read32.g);
			distance += System.Math.Abs(color32.b - read32.b);
			distance += System.Math.Abs(color32.a - read32.a);

			if (distance <= threshold32)
			{
				if (onColor != null)
				{
					onColor.Invoke(color);
				}
			}
		}
	}
}

#if UNITY_EDITOR
namespace PaintIn3D.Examples
{
	using UnityEditor;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(P3dReadColorEvent))]
	public class P3dReadColorEvent_Editor : P3dEditor<P3dReadColorEvent>
	{
		protected override void OnInspector()
		{
			Draw("color", "This color we want to detect.");
			Draw("threshold", "The RGBA values must be within this range of a color for it to be counted.");

			EditorGUILayout.Separator();

			Draw("onColor");
		}
	}
}
#endif