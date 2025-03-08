using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class containsBall : MonoBehaviour
{
    public event EventHandler OnBallhits;

    public event EventHandler OnBallGrabbed;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ball")
        {
            OnBallhits?.Invoke(this, EventArgs.Empty);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "ball")
        {
            OnBallGrabbed?.Invoke(this, EventArgs.Empty);
        }
    }
}
