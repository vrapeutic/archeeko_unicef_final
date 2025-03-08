using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopCamera : MonoBehaviour
{
    [SerializeField] Transform targetObject;
    Vector3 newPos = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        GetComponent<Camera>().enabled = false;
        GetComponent<AudioListener>().enabled = false;
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
