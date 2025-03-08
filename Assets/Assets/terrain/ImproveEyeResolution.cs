using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ImproveEyeResolution : MonoBehaviour
{
    public float Scale = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.XR.XRSettings.eyeTextureResolutionScale = 1 + Scale;       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
