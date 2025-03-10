﻿////////////////////////////////////////////////////////////////////////////////////////////////
//
//  MeshKitAutoLOD.cs
//
//	This component allows us to create LODs. This is a modified version of DecimateObject.cs
//	specifically designed for MeshKit.
//
//	© 2018 Melli Georgiou.
//	Hell Tap Entertainment LTD
//
////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.Rendering;
using HellTap.MeshDecimator.Unity;

namespace HellTap.MeshKit {

	[DisallowMultipleComponent]
	[AddComponentMenu("MeshKit/Automatic LOD")]
	public sealed class MeshKitAutoLOD : MonoBehaviour {

		// Should we use advanced mode in the editor?
		[HideInInspector]
		public bool advancedMode = false;

		// Decimation Options
		[HideInInspector]
		public bool preserveBorders = false;
		[HideInInspector]
		public bool preserveSeams = false;
		[HideInInspector]
		public bool preserveFoldovers = false;
		
		// Variables (these are public so it can be modified by the MeshKit GUI)
		[HideInInspector]
		public LODSettings[] levels = null;
		
		[HideInInspector]
		[Range(0f,99.9f)]
		public float cullingDistance = 1f;	// The LOD Distance where the object is culled

		[HideInInspector]
		public bool generated = false;

		////////////////////////////////////////////////////////////////////////////////////////////////
		//	LEVELS
		//	Gets or sets the LOD levels of this object.
		////////////////////////////////////////////////////////////////////////////////////////////////

		public LODSettings[] Levels {

			get { 
			
				// If we're using the "Easy" mode, always use the default settings
				if( advancedMode == false ){
					return new LODSettings[]{
						new LODSettings(0.8f, 50f, SkinQuality.Auto, true, ShadowCastingMode.On),
						new LODSettings(0.65f, 16f, SkinQuality.Bone2, true, ShadowCastingMode.Off, MotionVectorGenerationMode.Object, false),
						new LODSettings(0.4f, 7f, SkinQuality.Bone1, false, ShadowCastingMode.Off, MotionVectorGenerationMode.Object, false)
					};

				// Otherwise, use cutom setup
				} else {
					return levels; 
				}
			}

			set { levels = value; }
		}

		////////////////////////////////////////////////////////////////////////////////////////////////
		//	IS GENERATED
		//	Gets if this decimated object has been generated.
		////////////////////////////////////////////////////////////////////////////////////////////////

		public bool IsGenerated{

			get { return generated; }
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		//	RESET
		//	Resets the levels and settings to default
		////////////////////////////////////////////////////////////////////////////////////////////////

		public void Reset(){

			levels = new LODSettings[]{
				new LODSettings(0.8f, 50f, SkinQuality.Auto, true, ShadowCastingMode.On),
				new LODSettings(0.65f, 16f, SkinQuality.Bone2, true, ShadowCastingMode.Off, MotionVectorGenerationMode.Object, false),
				new LODSettings(0.4f, 7f, SkinQuality.Bone1, false, ShadowCastingMode.Off, MotionVectorGenerationMode.Object, false)
			};

			cullingDistance = 1f;
			ResetLODs();
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		//	GENERATE LODs
		//	Generates the LODs for this object. The statusCallback variable is a status report callback.
		////////////////////////////////////////////////////////////////////////////////////////////////

		public void GenerateLODs(LODStatusReportCallback statusCallback = null){

			if (levels != null){

				// LODGenerator.GenerateLODs(gameObject, levels, statusCallback);
				LODGenerator.GenerateLODs(gameObject, /*levels,*/ Levels, statusCallback, preserveBorders, preserveSeams, preserveFoldovers );	// <- Changed so easy / advanced mode works dynamically
			}
			generated = true;
		}

		////////////////////////////////////////////////////////////////////////////////////////////////
		//	RESET LODS
		//	Resets the LODs for this object.
		////////////////////////////////////////////////////////////////////////////////////////////////

		public void ResetLODs(){

			LODGenerator.DestroyLODs(gameObject);
			generated = false;
			advancedMode = false;
		}

	}
}