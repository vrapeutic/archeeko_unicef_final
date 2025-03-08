using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StickyArrow : MonoBehaviour
{
    [SerializeField] bool sticky = false;
    [SerializeField] float speed = 500.0f;
    [SerializeField] Transform tip = null;
    Rigidbody arrowRigidbody;
    [HideInInspector]
    public bool isStopped = true;
    Vector3 arrowLastPosition = Vector3.zero;
    WaitForSeconds waitTime;
    public Vector3 position;
    public Vector3 force;

    private void Start()
    {
        arrowRigidbody = GetComponent<Rigidbody>();
        waitTime = new WaitForSeconds(3);
        
        try
        {
        GameManager.OnEndSuccess += DisableArrow;
        GameManager.OnEndUnsuccess += DisableArrow;
        }
        catch(Exception e)
        {
            Console.WriteLine("couldn't access GameManager Script"+e.Message);
        }
    }

     private void Update()
    {
        if (!isStopped)
        {
           SpinObjectInAir();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);/*
        if (other.gameObject.GetComponent<MeshRenderer>().enabled == false)
        {
          other.gameObject.GetComponent<MeshRenderer>().enabled=true;
        }*/
        if (other.gameObject.tag != "StickyArrow")
        {
            Stop();
            if(!sticky)
            { 
               StartCoroutine(DisableArrowIEnum());
            }  
        }
          arrowLastPosition = tip.position;     
    }
     void Stop()
    {
        arrowRigidbody.velocity = Vector3.zero;
        arrowRigidbody.useGravity = false;
        arrowRigidbody.isKinematic = true;
        isStopped = true;
        Debug.Log("Stopped Arrow");
    }
    public void Fire(float pullingValue)
    {
        isStopped = false;
        Debug.Log("Fire is working just fine");
        transform.parent = null;
    #if UNITY_ANDROID
        arrowRigidbody.isKinematic = false;
        arrowRigidbody.useGravity = true;
        force = transform.forward * (pullingValue * speed);
        arrowRigidbody.AddForce(force);
    #endif
    }
    public IEnumerator DisableArrowIEnum()
    {
        yield return waitTime;
        DisableArrow();
    }
    private void DisableArrow()
    {
        gameObject.SetActive(false);
    }
    public void SpinObjectInAir()
    {
        float _yVelocity = arrowRigidbody.velocity.y;
        float _xVelocity = arrowRigidbody.velocity.x;
        float _zVelocity = arrowRigidbody.velocity.z;
        float _combinedVelocity = Mathf.Sqrt(_xVelocity * _xVelocity + _zVelocity * _zVelocity);
        float _fallAngel = -1 * Mathf.Atan2(_yVelocity, _combinedVelocity) * 180 / Mathf.PI;
        transform.eulerAngles = new Vector3(_fallAngel, transform.eulerAngles.y, transform.eulerAngles.z);
    }
    private void OnDisable()
    {   try
        {
          GameManager.OnEndSuccess -= DisableArrow;
          GameManager.OnEndUnsuccess -= DisableArrow;
        }
        catch(Exception e)
        {
            Console.WriteLine("couldn't access GameManager Script"+e.Message);
        }
    }
}