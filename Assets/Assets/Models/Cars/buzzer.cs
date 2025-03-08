using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buzzer : MonoBehaviour
{
    AudioSource serine;
    //GameObject[] points, nightPoints;
    //int i = 0 , j=0;
    public void Start()
    {
       // points = GameObject.FindGameObjectsWithTag("point");
        //nightPoints = GameObject.FindGameObjectsWithTag("pointNight");
        serine = this.GetComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "point")
        {
              //  points[i].SetActive(false);
                serine.Play();
               // i++;
        }

    }
}
