using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsSync : MonoBehaviour
{
    [SerializeField] Transform targetObject;
    Vector3 newPos = new Vector3();
    SkinnedMeshRenderer theMesh;
    void Start()
    {
        theMesh = GetComponent<SkinnedMeshRenderer>();
        theMesh.enabled=true;
#if UNITY_ANDROID       
        theMesh.enabled=false;
#endif
    }
    
    private void Update()
    {
#if UNITY_ANDROID
        newPos.x = targetObject.position.x;
        newPos.y = targetObject.position.y;
        newPos.z = targetObject.position.z;
        transform.position = newPos;
        transform.rotation = targetObject.rotation;
#endif
    }
}
