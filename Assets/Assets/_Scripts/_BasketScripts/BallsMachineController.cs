using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallsMachineController : MonoBehaviour
{
    public static BallsMachineController instance;
    public float speed;
    public GameObject[] balls;
    public int currentBallNo = 0;
    public Animator anim;
    AudioSource audioSource;
    IEnumerator coroutine;
    
    bool ended=false;
    string gameObjectName;
    public delegate void ReleaseAction();
    public static event ReleaseAction OnRelease;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {   try
        {
         anim = this.GetComponent<Animator>();
         audioSource = this.GetComponent<AudioSource>();
        }
        catch (NullReferenceException e)
        {
           Console.WriteLine("anim and/or audioSource = null"+e.Message);
        }

        coroutine = WaitAndTurnOff();

        gameObjectName = this.gameObject.name;

        try
        {
          GameManager.OnEndSuccess += DisableButton;
          GameManager.OnEndUnsuccess += DisableButton;
        }
        catch (Exception e)
        {
           Console.WriteLine("couldn't access GameManager"+e.Message);
        }
    }
    void DisableButton()
    {
       ended=true;
    }

    public void OnTriggerEnter(Collider other)
    {
        UnityEngine.Debug.Log(" button is pressed");
        this.GetComponent<Collider>().enabled = false;
        anim.SetBool("pressed", true);
        StartCoroutine(coroutine);
        audioSource.Play();
        RollNewBall();
        OnRelease();
    }
    void RollNewBall()
    {
        if (currentBallNo<balls.Length)
        {
         balls[currentBallNo].SetActive(true);
         var ballRigidbody =balls[currentBallNo].GetComponent<Rigidbody>();

#if UNITY_ANDROID
        
         ballRigidbody.velocity = balls[currentBallNo].transform.forward * speed;
#endif
         balls[currentBallNo].transform.parent = null;
         try
         {
          Statistics.instance.tries--;

          if (Statistics.instance.tries > 0 && Player.instance.canPlay)
          {
            currentBallNo++;
          }
          else if (Statistics.instance.tries == 0)
          {
            try
            {  
              balls[currentBallNo].GetComponent<LastProjectile>().ReleaseLastProjectile();
            }
            catch (Exception e)
            {
              Console.WriteLine("couldn't access LastProjectile Script"+e.Message);
            }
          }
         }
        catch (Exception e)
        {
           Console.WriteLine("couldn't access statistics to get current tries number"+e.Message);
        }
        }
        else 
        {
           UnityEngine.Debug.Log("Out of Balls");
        }
    }
    public void ReactivePushButton()
    {
        if(!ended)
        {
            this.GetComponent<Collider>().enabled = true;
        }
    }
    IEnumerator WaitAndTurnOff()
    {
        yield return new WaitForSeconds(0.05f);
        anim.SetBool("pressed", false);
        UnityEngine.Debug.Log("coroutine started");
    }
}
