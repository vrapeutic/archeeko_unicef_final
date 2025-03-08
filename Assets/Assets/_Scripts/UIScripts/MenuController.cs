using System.Net.NetworkInformation;
using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    //[SerializeField] GameObject playButton;
    void Start()
    {
        Statistics.instance.OnStartFunc();
    }
    public void ShowInstructionsUI(GameObject instructionsToShow)
    {
            instructionsToShow.SetActive(true);
    }
    public void DisableInstructionsUI(GameObject instructionsToShow)
    {
            instructionsToShow.SetActive(false);
    }
    public void Setlevel(int level)
    {
        Statistics.instance.level = level;
    }

    public void SetType(bool isClosedTime)
    {
        Statistics.instance.isClosedTime = isClosedTime;
    }

    public void Exit()
    {
        Application.Quit();
    }


    public void SetGame(bool isArchery)
    {
        Statistics.instance.isArchery = isArchery;
        if (isArchery) UnityEngine.Debug.Log("Archery game seleced");
        else UnityEngine.Debug.Log("paint gun game seleced");
    }

    public void SetEnvironment(int enviroment)
    {
        SetEnvironmentRPC(enviroment);
        switch (enviroment)
        {
            case 1:
                UnityEngine.Debug.Log("Tree enviroment selected");
                break;
            case 2:
                UnityEngine.Debug.Log("Garden enviroment selected");
                break;
            case 3:
                UnityEngine.Debug.Log("PlayRoom enviroment selected");
                break;
            default:
                break;
        }
    }

    public void SetEnvironmentRPC(int enviroment)
    {
        Statistics.instance.enviroment = enviroment;
    }
    public void SetMood(bool isDay)
    {
            SetMoodRPC(isDay);

    }
    public void SetMoodRPC(bool isDay)
    {

        Statistics.instance.isDay = isDay;
        UnityEngine.Debug.Log("mood is set");

    }

    public void SetClosedTimeValue(int closedTimeValue)
    {
        Statistics.instance.closedTimeValue = closedTimeValue;
    }

    public void SetTargetDistance(int distance)
    {
        Statistics.instance.targetDepth = distance;
    }

    public void SetTriesNo(int tries)
    {
        Statistics.instance.intialTries = Statistics.instance.tries = tries;
    }


    public void SelectCharacter(int characterIndex)
    {
        Statistics.instance.character = characterIndex;
    }
  

    public void LoadSceneOptimized()
    {
        SceneManager.LoadSceneAsync("Garden Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main");
    }
    public void LoadLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

}
