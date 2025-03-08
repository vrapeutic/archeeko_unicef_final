using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSettings : MonoBehaviour
{

    [SerializeField]
    bool reuseCollisionCallbacks;
    [SerializeField]
    bool autoSyncTransform;
    [SerializeField]
    public int targetFrameRate = 30;
    void Start()
    {
        Physics.reuseCollisionCallbacks = reuseCollisionCallbacks;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }
   

  


}
