﻿using System.Collections.Generic;
using UnityEngine;

namespace PaintIn3D
{
	/// <summary>This component allows you to define a set of <b>P3dPaintableTexture</b> and <b>P3dMaterial</b> components that are configured for a specific set of Materials.</summary>
	[HelpURL(P3dHelper.HelpUrlPrefix + "P3dPreset")]
	[AddComponentMenu("")]
	public class P3dPreset : MonoBehaviour
	{
		/// <summary>This allows you to name this preset.
		/// None/null = The GameObject name will be used.</summary>
		public string Title { set { title = value; } get { return title; } } [SerializeField] private string title;

		/// <summary>Automatically add the <b>P3dMaterialCloner</b>.</summary>
		public bool AddMaterialCloner { set { addMaterialCloner = value; } get { return addMaterialCloner; } } [SerializeField] private bool addMaterialCloner = true;

		private static List<P3dPreset> cachedPresets;

		/// <summary>This gives you a list of all presets in the project.
		/// NOTE: This is editor-only.</summary>
		public static List<P3dPreset> CachedPresets
		{
			get
			{
				if (cachedPresets == null)
				{
					cachedPresets = new List<P3dPreset>();
#if UNITY_EDITOR
					var guids = UnityEditor.AssetDatabase.FindAssets("t:prefab");

					foreach (var guid in guids)
					{
						var prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(UnityEditor.AssetDatabase.GUIDToAssetPath(guid));

						if (prefab != null)
						{
							var preset = prefab.GetComponent<P3dPreset>();

							if (preset != null)
							{
								cachedPresets.Add(preset);
							}
						}
					}
#endif
				}

				return cachedPresets;
			}
		}

		public string FinalName
		{
			get
			{
				return string.IsNullOrEmpty(title) == false ? title : name;
			}
		}

#if UNITY_EDITOR
		/// <summary>This method returns true if this preset is designed for the specified shader.</summary>
		public bool Targets(Shader target)
		{
			if (target != null)
			{
				var candidates = GetComponents<P3dPaintableTexture>();

				foreach (var candidate in candidates)
				{
					var groupData = P3dGroupData_Editor.GetGroupData(candidate.Group);
				
					if (groupData != null)
					{
						if (groupData.Supports(target) == false)
						{
							return false;
						}
					}
				}

				return true;
			}

			return false;
		}
#endif

		public bool CanAddTo(P3dPaintable paintable, int index)
		{
			var candidates = GetComponents<P3dPaintableTexture>();

			foreach (var paintableTexture in paintable.GetComponents<P3dPaintableTexture>())
			{
				if (paintableTexture.Slot.Index == index && HasPaintableTexture(candidates, paintableTexture) == true)
				{
					return false;
				}
			}

			return true;
		}

		private bool HasPaintableTexture(P3dPaintableTexture[] candidates, P3dPaintableTexture paintableTexture)
		{
			foreach (var candidate in candidates)
			{
				if (candidate.Slot.Name == paintableTexture.Slot.Name)
				{
					return true;
				}
			}

			return false;
		}

		private bool HasMaterialCloner(P3dPaintable paintable, int index)
		{
			foreach (var materialCloner in paintable.GetComponents<P3dMaterialCloner>())
			{
				if (materialCloner.Index == index)
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>This method applies the preset components to the specified paintable.
		/// NOTE: This is editor-only.</summary>
		public void AddTo(P3dPaintable paintable, Shader shader, int index, int stateLimit)
		{
#if UNITY_EDITOR
			if (addMaterialCloner == true && HasMaterialCloner(paintable, index) == false)
			{
				var newMaterialCloner = paintable.gameObject.AddComponent<P3dMaterialCloner>();

				newMaterialCloner.Index = index;
			}

			foreach (var paintableTexture in GetComponents<P3dPaintableTexture>())
			{
				if (UnityEditorInternal.ComponentUtility.CopyComponent(paintableTexture) == true)
				{
					var newPaintableTexture = paintable.gameObject.AddComponent<P3dPaintableTexture>();

					UnityEditorInternal.ComponentUtility.PasteComponentValues(newPaintableTexture);

					var groupData = P3dGroupData_Editor.GetGroupData(paintableTexture.Group);
					var slotName  = newPaintableTexture.Slot.Name;

					if (groupData != null && shader != null)
					{
						groupData.TryGetShaderSlotName(shader.name, ref slotName);
					}

					newPaintableTexture.Slot = new P3dSlot(index, slotName);

					if (stateLimit >= 0)
					{
						if (newPaintableTexture.UndoRedo != P3dPaintableTexture.UndoRedoType.LocalCommandCopy)
						{
							newPaintableTexture.UndoRedo   = P3dPaintableTexture.UndoRedoType.FullTextureCopy;
							newPaintableTexture.StateLimit = stateLimit;

							UnityEditor.EditorUtility.SetDirty(newPaintableTexture);
						}
					}
				}
			}
#endif
		}
	}
}

#if UNITY_EDITOR
namespace PaintIn3D
{
	using UnityEditor;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(P3dPreset))]
	public class P3dPreset_Editor : P3dEditor<P3dPreset>
	{
		protected override void OnInspector()
		{
			if (P3dPreset.CachedPresets.Contains(Target) == false && P3dHelper.IsAsset(Target) == true)
			{
				P3dPreset.CachedPresets.Add(Target);
			}

			EditorGUILayout.HelpBox("You can use this preset from the Paint in 3D window after making an object paintable.", MessageType.Info);

			Draw("title", "This allows you to name this preset.\n\nNone/null = The GameObject name will be used.");
			Draw("addMaterialCloner", "Automatically add the P3dMaterialCloner.");

			if (AnyDrawInvalidIndex() == true)
			{
				EditorGUILayout.Separator();

				EditorGUILayout.HelpBox("P3dPaintableTexture slot index values should be 0 for presets, because they will be overwritten when added.", MessageType.Warning);
			}
		}

		private bool AnyDrawInvalidIndex()
		{
			foreach (var T in Targets)
			{
				foreach (var paintableTexture in T.GetComponents<P3dPaintableTexture>())
				{
					if (paintableTexture.Slot.Index != 0)
					{
						return true;
					}
				}
			}

			return false;
		}

		[MenuItem("Assets/Create/Paint in 3D/Preset")]
		private static void CreateAsset()
		{
			var preset = new GameObject("Preset").AddComponent<P3dPreset>();
			var guids  = Selection.assetGUIDs;
			var path   = guids.Length > 0 ? AssetDatabase.GUIDToAssetPath(guids[0]) : null;

			if (string.IsNullOrEmpty(path) == true)
			{
				path = "Assets";
			}
			else if (AssetDatabase.IsValidFolder(path) == false)
			{
				path = System.IO.Path.GetDirectoryName(path);
			}

			var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/NewPreset.prefab");
			var asset            = PrefabUtility.SaveAsPrefabAsset(preset.gameObject, assetPathAndName);

			DestroyImmediate(preset.gameObject);

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			EditorUtility.FocusProjectWindow();

			Selection.activeObject = asset; EditorGUIUtility.PingObject(asset);
		}
	}
}
#endif