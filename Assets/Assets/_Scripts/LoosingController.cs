using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoosingController : MonoBehaviour
{
    [SerializeField]
    GameEvent playerLoose;

    private void Start()
    {

        Statistics.instance.tries = Statistics.instance.intialTries;
    }
    public void CheckTries()
    {
        if(Statistics.instance.tries <= 0 && Statistics.instance.remainingPrizes > 0)
        {
            playerLoose.Raise();
        }
    }

}
