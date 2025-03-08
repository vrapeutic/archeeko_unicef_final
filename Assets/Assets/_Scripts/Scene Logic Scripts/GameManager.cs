using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonoBehaviour
{
    [SerializeField] IntVariable gifts;
    [SerializeField] GameEvent onEndUnSeccessfully;
    [SerializeField] GameEvent onEndSeccessfully;
    public static GameManager instance;
    // we need to optimize seccuss logic
    public delegate void EndUnsuccessAction();
    public static event EndUnsuccessAction OnEndUnsuccess;
    public delegate void EndsuccessAction();
    public static event EndsuccessAction OnEndSuccess;
    //JsonItems JsonItemsInstance = new JsonItems();
    bool canEnterEndingAttemptInum = true;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (GetSceneName(0) == "SystemLobby") Debug.Log("helw");
        else Debug.Log("meshhelw");
        StatisticsJsonFile.Instance.data.attempt_start_time = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt");
       // Statistics.instance.tries = Statistics.instance.intialTries;
        canEnterEndingAttemptInum = true;
    }

    public void EndingUnSuccessful()
    {
        OnEndUnsuccess();
        onEndUnSeccessfully.Raise();
        StartCoroutine(EndingAttemptInum());
    }

    public void EndingSuccessful()
    {
        //Debug.Log("GameManager.EndingSuccessful()");
        OnEndSuccess();
        onEndSeccessfully.Raise();
        StartCoroutine(EndingAttemptInum());
    }


    IEnumerator EndingAttemptInum()
    {
        Player.instance.canPlay = false;
        if (!canEnterEndingAttemptInum) yield break;
        canEnterEndingAttemptInum = false;
        yield return new WaitForSeconds(3);
        if (GetSceneName(0) == "SystemLobby") Application.Quit();
    }
    #region check success cases
    public void CheckRemainingPrizes()
    {
        gifts.Value = gifts.Value - 1;
        Statistics.instance.remainingPrizes = gifts.Value;
        if (gifts.Value <= 0) EndingSuccessful();
    }
    #endregion

    string GetSceneName(int index)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(index);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }
}


//#region when doctor user choose to end session without success or not cases

//public bool isMainMenuClicked = true;

//public void SetIsMainMenuClicked(bool clicked)
//{
//    isMainMenuClicked = clicked;
//}

//public void OnClickYesToSaveData()
//{
//    OnClickYesToSaveDataRPC();
//}

//public void OnClickYesToSaveDataRPC()
//{
//    StartCoroutine(OnClickYesToSaveDataIenum());
//}

//IEnumerator OnClickYesToSaveDataIenum()
//{
//    if (Statistics.instance.android) Statistics.instance.SendAttemptStatistics();
//    yield return new WaitForSeconds(3);
//    if (!Statistics.instance.android) OnClickNoToSaveData();
//}

//public void OnClickNoToSaveData()
//{
//    if (isMainMenuClicked) LoadMainMenu();
//    else ExitModule();
//}


//public void ExitModule()
//{
//    Application.Quit();
//}

//#endregion
