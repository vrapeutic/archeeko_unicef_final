using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowInfo : MonoBehaviour
{
    //this script is responsable for show info
    [SerializeField] Text NoOfTriesText;
    [SerializeField] GameObject timer;

    WaitForSeconds asecound;

    void Start()
    {
        asecound = new WaitForSeconds(1f);
          BowTest.OnRelease += OnReleaseFunc;
          PaintIn3D.GunInput.OnRelease += OnReleaseFunc;
          BallsMachineController.OnRelease += OnReleaseFunc;
          NoOfTriesText.text = Statistics.instance.tries.ToString();
          if (Statistics.instance.isClosedTime)
          {
            if (Statistics.instance.android) StartCoroutine(ActivateTimerIenum());
          }
    }
    void OnReleaseFunc()
    {
        OnReleaseeFunc(Statistics.instance.tries);    
    }
    public void OnReleaseeFunc(int tries)
    {
        NoOfTriesText.text = tries.ToString();
    }

    IEnumerator ActivateTimerIenum()
    {
            yield return new WaitForSeconds(2);
            ActivateTimer();
    }
    public void ActivateTimer()
    {
        timer.SetActive(true);
        StartCoroutine(CounterDown());
    }

    IEnumerator CounterDown()
    {
        int currentTime = Statistics.instance.closedTimeValue;
        while (currentTime > 0)
        {
            UpdateText(currentTime);
            yield return asecound;
            currentTime--;
        }
        UpdateText(currentTime);

          if (Statistics.instance.android)
          {
           // Debug.Log("Player.instance.canPlay: "+Player.instance.canPlay);
              if (Player.instance.canPlay)
              {
                    GameManager.instance.EndingUnSuccessful();
              }
          }
    }

    void UpdateText( int value )
    {
        timer.GetComponentInChildren<Text>().text = value.ToString();
    }

    private void OnDisable()
    {
        BowTest.OnRelease -= OnReleaseFunc;
        PaintIn3D.GunInput.OnRelease -= OnReleaseFunc;
        BallsMachineController.OnRelease -= OnReleaseFunc;
    }
}

