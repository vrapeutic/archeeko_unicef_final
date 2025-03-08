using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseTimeTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Arrow" || other.tag == "ball")
        {
            //i think it doesn't matter if the remaining prizes>0 or not, he can't release any arrow if it is <= 0 
            //so i'll comment this if condition and make it true if any ball or arrow hits 
            //if (Statistics.instance.remainingPrizes > 0) Statistics.instance.responeTimeBool = true;
            if (Statistics.instance)
                Statistics.instance.responeTimeBool = true;
            Debug.Log("responseTimeHitted");

        }
    }
}
