using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Score : MonoBehaviour
{
    List<string> Projectiles = new List<string>();
    bool exist = false;
    [SerializeField]
    GameEvent dartBoardHitted;
    [SerializeField]
    GameEvent dartFinish;
  
    private void Start()
    {
        Statistics.instance.remainingPrizes = 10;
    }
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("collison name " + other.gameObject.tag);     
        if (Statistics.instance.android)
        {
            if (other.gameObject.tag == "ball"||other.gameObject.tag == "StickyArrow")
            {
               foreach (string p in Projectiles) 
               {
                   if(p==other.gameObject.name)
                   {
                     exist=true;
                   }
               }
               if(!exist)
               {
                 HitGoal();
                 Projectiles.Add(other.gameObject.name);
               }
               exist=false;
            }      
        }
     
     
    }

    public void HitGoal()
    {
        if (gameObject.name=="BasketInsider")
        {
            GetComponent<AudioSource>().Play();
        }

        Debug.Log("Hit the board");
     
        Statistics.instance.score++;            
        Statistics.instance.remainingPrizes--;      

        Debug.Log("remainingPrizes" + Statistics.instance.remainingPrizes);
        Debug.Log("Targets Hit : " + (10 - Statistics.instance.remainingPrizes));
        
      
         Statistics.instance.prizeHited = true;      
        
        Debug.Log("Statistics.instance.remainingPrizes :" + Statistics.instance.remainingPrizes);

       
         if (Statistics.instance.remainingPrizes <= 0)
         {
            dartFinish.Raise();
            if (Statistics.instance.level == 3)
            {          
               Player.instance.CheckPlayerHealth();          
            } 
            else
            {              
               GameManager.instance.EndingSuccessful();              
            }
         }     
         else if (Statistics.instance.remainingPrizes > 0)
         {
            dartBoardHitted.Raise();
            Statistics.instance.responeTimeBool = true;                     
         }

        
       
       
    }
}
