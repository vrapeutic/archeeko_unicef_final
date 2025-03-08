using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LastProjectile : MonoBehaviour
{
    public void ReleaseLastProjectile()
    {
        Debug.Log("release last arrow has been called");
        
        try
        {
         if (Statistics.instance.remainingPrizes == 1)
         {
           Debug.Log("remaining Prize =1");
           StartCoroutine(CheckPrizeHitted());
         }
               
        else
        {
          StartCoroutine(WaitAndLoseInum());   
        }
        }
        catch (Exception e)
        {
          Console.WriteLine("couldn't access statistics to check remaining prizes"+e.Message);
        } 
    }
    IEnumerator CheckPrizeHitted()
    {
         Statistics.instance.prizeHited = false;
         yield return new WaitForSeconds(4);
         if (Statistics.instance.prizeHited)
         {
            Player.instance.CheckPlayerHealth();
         }
         else
         {
           try
           {  
              GameManager.instance.EndingUnSuccessful();
           }
           catch (Exception e)
           {
             Console.WriteLine("couldn't access statistics to check remaining prizes"+e.Message);
           } 
         }
    }
    IEnumerator WaitAndLoseInum()
    {
        Debug.Log("WaitAndLoseInum has been called");
        yield return new WaitForSeconds(1);
        try
        {
         GameManager.instance.EndingUnSuccessful();
        }
        catch (Exception e)
        {
          Console.WriteLine("couldn't access game manager to end unsucessful"+e.Message);
        } 
    }
}
