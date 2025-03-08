using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidedWith : MonoBehaviour
{
    Rigidbody rigidbody;
    void Start()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
        if (rigidbody==null)
        {
            this.gameObject.AddComponent<Rigidbody>();
        }
    }

    void onTriggerEnter (Collider other)
     {
       other.gameObject.GetComponent<MeshRenderer>().enabled=true;
       Debug.Log(this.gameObject.name + " collided with " +other.gameObject.name);
    }
    
    void OnCollisionEnter(Collision other)
    {
        Debug.Log(this.gameObject.name + " collided with " +other.gameObject.name);
        rigidbody.velocity = Vector3.zero;
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
        other.gameObject.GetComponent<MeshRenderer>().enabled=true;
    
    }
}