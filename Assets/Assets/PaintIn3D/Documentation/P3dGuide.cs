﻿using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PaintIn3D.Examples
{
	/// <summary>This class defines documentation data that can be viewed in the inspector.</summary>
	public class P3dGuide : ScriptableObject
	{
		public string LongName;

		public string ShortName;

		public TextAsset Documentation;

		[System.NonSerialized]
		private Texture2D icon;

		[System.NonSerialized]
		private string version;

		public Texture2D Icon
		{
			get
			{
				if (icon == null)
				{
					icon = new Texture2D(1, 1);

					icon.LoadImage(System.Convert.FromBase64String("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"));
				}

				return icon;
			}
		}

		public string Version
		{
			get
			{
				if (version == null && Documentation != null)
				{
					var text = Documentation.text;
					var a    = text.IndexOf("Documentation - ");
					var b    = text.IndexOf("</title>");

					if (a > 0 && b > 0)
					{
						version = text.Substring(a + 16, b - a - 16);
					}
				}

				return version;
			}
		}
	}
}

#if UNITY_EDITOR
namespace PaintIn3D.Examples
{
	[CustomEditor(typeof(P3dGuide))]
	public class P3dGuide_Inspector : Editor
	{
		private static GUIStyle titleStyle;

		private static GUIStyle headerStyle;

		private static GUIStyle bodyStyle;

		private static GUIStyle rateStyle;

		public static void UpdateStyles()
		{
			if (bodyStyle == null)
			{
				bodyStyle = new GUIStyle(EditorStyles.label);
				bodyStyle.wordWrap = true;
				bodyStyle.fontSize = 14;

				titleStyle = new GUIStyle(bodyStyle);
				titleStyle.fontSize = 26;
				titleStyle.alignment = TextAnchor.MiddleCenter;

				headerStyle = new GUIStyle(bodyStyle);
				headerStyle.fontSize = 18 ;

				rateStyle = new GUIStyle(EditorStyles.toolbarButton);

				rateStyle.fontSize = 20;
			}
		}

		public override void OnInspectorGUI()
		{
			var tgt = (P3dGuide)target;

			UpdateStyles();

			EditorGUI.EndDisabledGroup();

			EditorGUILayout.LabelField("Thank You For Using " + tgt.LongName + "!", headerStyle);
			EditorGUILayout.LabelField("The documentation can be opened below. To understand how this asset works I also recommend you run the example scenes, and read their description text.", bodyStyle);

			if (GUILayout.Button(new GUIContent("Local Documentation", "Open In Web Browser")) == true)
			{
				System.Diagnostics.Process.Start(Application.dataPath + AssetDatabase.GetAssetPath(tgt.Documentation).Remove(0, "Assets".Length));
			}

			if (GUILayout.Button(new GUIContent("Online Documentation", "Open In Web Browser")) == true)
			{
				Application.OpenURL("http://CarlosWilkes.com/Documentation/" + tgt.ShortName);
			}

			EditorGUILayout.Separator();
			EditorGUILayout.Separator();

			EditorGUILayout.LabelField("Need Help?", headerStyle);
			EditorGUILayout.LabelField("If you have questions, feel free to ask!", bodyStyle);

			if (GUILayout.Button(new GUIContent("Forum Thread", "Unity Forums")) == true)
			{
				Application.OpenURL("http://CarlosWilkes.com/Forum/" + tgt.ShortName);
			}
				
			if (GUILayout.Button(new GUIContent("E-Mail Me", "carlos.wilkes@gmail.com")) == true)
			{
				Application.OpenURL("mailto:carlos.wilkes@gmail.com");
			}

			if (GUILayout.Button(new GUIContent("Private Message", "Unity Forum Profile")) == true)
			{
				Application.OpenURL("http://forum.unity.com/members/41960");
			}

			EditorGUILayout.Separator();
			EditorGUILayout.Separator();

			EditorGUILayout.LabelField("You're Awesome!", headerStyle);
			EditorGUILayout.LabelField("If you haven't already, consider rating this asset. It really helps me out!", bodyStyle);

			if (GUILayout.Button(new GUIContent("Rate This Asset", tgt.LongName + " Asset Page")) == true)
			{
				Application.OpenURL("http://CarlosWilkes.com/Get/" + tgt.ShortName);
			}

			EditorGUILayout.Separator();
			EditorGUILayout.Separator();

			EditorGUILayout.LabelField("Made Something Cool?", headerStyle);
			EditorGUILayout.LabelField("Let me know so I can shout your project out!", bodyStyle);

			if (GUILayout.Button(new GUIContent("E-Mail Me", "carlos.wilkes@gmail.com")) == true)
			{
				Application.OpenURL("mailto:carlos.wilkes@gmail.com");
			}

			EditorGUILayout.Separator();
			EditorGUILayout.Separator();

			EditorGUILayout.LabelField("Want More?", headerStyle);
			EditorGUILayout.LabelField("I've released a range of assets to speed up your project development, check them out:", bodyStyle);

			if (GUILayout.Button(new GUIContent("My Website", "CarlosWilkes.com")) == true)
			{
				Application.OpenURL("http://CarlosWilkes.com");
			}
		}

		protected override void OnHeaderGUI()
		{
			var tgt = (P3dGuide)target;

			UpdateStyles();

			GUILayout.BeginHorizontal("In BigTitle");
			{
				var iconWidth = Mathf.Min(EditorGUIUtility.currentViewWidth/3f - 20f, 128f);
				var content   = new GUIContent("Version\n" + tgt.Version);
				var height    = Mathf.Max(titleStyle.CalcHeight(content, EditorGUIUtility.currentViewWidth - iconWidth), iconWidth);

				if (tgt.Icon != null)
				{
					GUILayout.Label(tgt.Icon, EditorStyles.centeredGreyMiniLabel, GUILayout.Width(iconWidth), GUILayout.Height(iconWidth));
				}

				GUILayout.Label(content, titleStyle, GUILayout.Height(height));
			}
			GUILayout.EndHorizontal();
		}
	}
}
#endif