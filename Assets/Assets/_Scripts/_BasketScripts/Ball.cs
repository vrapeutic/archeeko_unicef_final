using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    void isGrabbed_Listener(object sender, EventArgs e)
    {
        try
        {
          BallsMachineController.instance.ReactivePushButton();
        }
        catch (Exception ex)
        {
          Console.WriteLine("couldn't access statistics to update the response time"+ex.Message);
        }   
    }

    void isReleased_Listener(object sender, EventArgs e)
    {
        try
        {
          BallsMachineController.instance.ReactivePushButton();
        }
        catch (Exception ex)
        {
          Console.WriteLine("couldn't access BallsMachineController"+ex.Message);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "ball pot")
        {
            try
            {
             Statistics.instance.StopResponseTimeCounter();
            }
            catch (Exception e)
            {
                Console.WriteLine("couldn't access statistics to update the response time"+e.Message);
            }
        }
    }
}
