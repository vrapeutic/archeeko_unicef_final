using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Gift : MonoBehaviour
{
    bool exist = false;
    List<string> Projectiles = new List<string>();

    [SerializeField] GameEvent OnGiftHitted;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collison name " + other.gameObject.tag);

        if (other.tag == "Arrow")
        {
            Debug.Log("one Arrow hitted one gift");
            GetComponent<Collider>().enabled = false;
            HitGift();
        }
    }

    public virtual void HitGift()
    {
        
        GetComponentInChildren<ParticleSystem>().Play();
        Statistics.instance.score++;
        //GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = true;
        // Debug.Log("Tar : Targets Hit : "+(10- Statistics.instance.remainingPrizes));
        Statistics.instance.prizeHited = true;
        Debug.Log("Statistics.instance.remainingPrizes :" + Statistics.instance.remainingPrizes);
        GetComponent<AudioSource>().Play();

        if (Statistics.instance.remainingPrizes > 0)
        {
            Statistics.instance.responeTimeBool = true;
        }
        else
        {
            //  OnGameFinished();
            GameManager.instance.EndingSuccessful();
           
        }

        OnGiftHitted.Raise();
        Destroy(this.gameObject, 2);
    }

}