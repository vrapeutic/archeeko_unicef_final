using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllerChar : MonoBehaviour
{
    Animator currentAnim;

    //controllers
    bool giftHitted = false;
    bool playerHitted = false;
    bool canPlay = true;
    int remaningArchsWarning = 7;
    int warningNo = 0;
    int ballWarnningPhaseNo = 0;
    int bravoNo = 0;
    [SerializeField] GameEvent gameFinished;
    // Start is called before the first frame update
    void Start()
    {
        currentAnim = GetComponent<Animator>();
        if (Statistics.instance.intialTries == 15) remaningArchsWarning = 5;
        else if (Statistics.instance.intialTries == 20) remaningArchsWarning = 7;
        else remaningArchsWarning = 10;
        StartCoroutine(WelcomeIEnum());
        canPlay = true;
    }
    private void OnEnable()
    {
        Bow.OnRelease += OnReleaseFunc;
        //Enemy.OnAttack +=;
        Player.OnPlayerHitted += OnPlayerHittedFunc;
        GameManager.OnEndSuccess += OnEndSuccessFunc;
        GameManager.OnEndUnsuccess += OnEndUnsuccessFunc;
    }

    public void OnPlayerWon()
    {
        if(!Statistics.instance.android)
        {
            gameFinished.Raise();
        }
        
    }

    public void AnimatorSetIntger(int setIntger)
    {
        currentAnim.SetInteger("ControllerInt", setIntger);
        StartCoroutine(ResetAnimatorSetIntger());
    }


    IEnumerator ResetAnimatorSetIntger()
    {
        yield return new WaitForSeconds(.5f);
        currentAnim.SetInteger("ControllerInt", 0);
    }

    //states for animator

    //welcoming
    IEnumerator WelcomeIEnum()
    {
        //Debug.Log("WelcomeIEnum()");
        yield return new WaitForSeconds(5f);
        AnimatorSetIntger(1);
        Debug.Log("WelcomeIEnum");

    }

    //when release and not hit the gift 
    void OnReleaseFunc()
    {
        //Debug.Log("OnReleaseFunc()");
        if (!canPlay) return;
        StartCoroutine(CheckGiftHitted());
    }

    IEnumerator CheckGiftHitted()
    {
        giftHitted = false;
        yield return new WaitForSeconds(2f);
        if (!canPlay) yield break;
        if (!giftHitted)
        {//minimize warning to quarter
            if (warningNo % 4 == 0)
            {
                if (Statistics.instance.tries <= remaningArchsWarning)
                {
                    AnimatorSetIntger( 5);
                }
                else
                {
                    AnimatorSetIntger(4);
                }//AnimatorSetIntger(4);//lazm tnchn sah
            }
            warningNo++;
        }
    }
    //when hit gift and ball doesn`t hit player
    public void OnGiftHittedFunc()
    {
        giftHitted = true;
        if (!canPlay) return;
        if (Statistics.instance.level == 3)
        {
            if (ballWarnningPhaseNo % 4 == 0)
            {
                AnimatorSetIntger( 6);
            }
            ballWarnningPhaseNo++;
            StartCoroutine(CheckPlayerHitted());
        }
        else if (Statistics.instance.level != 3)
        {
            SayBravo();
        }//AnimatorSetIntger(7); 
    }
    IEnumerator CheckPlayerHitted()
    {
        playerHitted = false;
        yield return new WaitForSeconds(4f);
        if (!canPlay) yield break;
        if (!playerHitted)
        {
            SayBravo();
        }//AnimatorSetIntger(7);//Bravo
    }

  public  void SayBravo()
    {
     
        {
            AnimatorSetIntger( 7);
        }
        bravoNo++;
    }

    //when player hitted
    void OnPlayerHittedFunc()
    {
        playerHitted = true;
    }

    void OnEndUnsuccessFunc()
    {
        canPlay = false;
        StartCoroutine(OnEndIEnum(8));
    }

    void OnEndSuccessFunc()
    {
        canPlay = false;
        StartCoroutine(OnEndIEnum(9));
    }
    IEnumerator OnEndIEnum(int state)
    {
        yield return new WaitForSeconds(1f);
        AnimatorSetIntger(state);
    }


 

    private void OnDisable()
    {
        Bow.OnRelease -= OnReleaseFunc;
        //Enemy.OnAttack -=;
        Player.OnPlayerHitted -= OnPlayerHittedFunc;
        GameManager.OnEndSuccess -= OnEndSuccessFunc;
        GameManager.OnEndUnsuccess -= OnEndUnsuccessFunc;
    }
    public void OnSayBravo()
    {
            if (Statistics.instance.remainingPrizes == 0 &&Statistics.instance.level ==3)
            {
                AnimatorSetIntger( 9);
            }
    }
}
