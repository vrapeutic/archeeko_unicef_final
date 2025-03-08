using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BirdMovementTrigger : MonoBehaviour
{
    [SerializeField] Animator bird;
    [SerializeField] Animator birdParent;

    Statistics stats;
    private void Start()
    {
        //stats = Statistics.instane;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            Debug.Log("Bird flying away from Flower!");
            //if (BirdController.isBirdOnFlower)
            {
                //stats.birdFlyingResponseTimes++;
                Debug.Log("Bird should fly");
                //Debug.Log("Response Time : Bird Response Times : " + stats.birdFlyingResponseTimes);
                //BirdController.isBirdOnFlower = false;
                FlyingBirdAnim();
            } 
        }
    }

    private void FlyingBirdAnim()
    {
            FlyingBirdAnimAndroid();       
    }

    private void FlyingBirdAnimAndroid()
    {
        Debug.Log("bird animation function called");
        birdParent.SetFloat("speed", 1.0f);
        bird.SetInteger("BirdAnimation", 0);
    }
}
