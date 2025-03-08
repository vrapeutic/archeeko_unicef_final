using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartBoardTargets : MonoBehaviour
{
    [SerializeField]
    GameEvent dartBoardTargetHitted;
    [SerializeField]
    GameEvent dartBoardAllTargetsHitted;

    int totalTargets;

    private void Start()
    {
        totalTargets = 10;
    }

    private void OnTriggerEnter(Collider other)
    {
     if(other.CompareTag("StickyArrow") )
        {
            other.GetComponent<Collider>().enabled = false;
            Statistics.instance.score++;
            Statistics.instance.remainingPrizes--;
            totalTargets--;
            //  other.GetComponent<StickyArrow>().Stop();
            if (totalTargets > 0) HitRPC();
            else GameFinshRPC();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Collider>().enabled = false;
        if(Statistics.instance.android)
        {
            if (collision.gameObject.CompareTag("ball") || collision.gameObject.CompareTag("StickyArrow"))
            {
                Statistics.instance.score++;
                Statistics.instance.remainingPrizes--;
                totalTargets--;
                if (totalTargets > 0) HitRPC();
                else GameFinshRPC();

            }
        }
       
    }
    public void HitRPC()
    {
        dartBoardTargetHitted.Raise();
    }

    public void GameFinshRPC()
    {
        dartBoardAllTargetsHitted.Raise();
    }
}
