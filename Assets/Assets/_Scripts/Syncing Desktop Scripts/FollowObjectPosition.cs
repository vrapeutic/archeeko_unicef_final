using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectPosition : MonoBehaviour
{
    [SerializeField] Transform targetObjetc;
    Vector3 newPos = new Vector3();
    private void Update()
    {
        newPos.x = targetObjetc.position.x;
        newPos.y = targetObjetc.position.y - .9f;
        newPos.z = targetObjetc.position.z;
        transform.position = newPos;
    }
}
