using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseEventWhenTriggerEnter : MonoBehaviour
{
    [SerializeField]GameEvent eventToRaise;
    [SerializeField]string tagToComare;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagToComare)) eventToRaise.Raise();
    }

}
