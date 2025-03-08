using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerRequest : MonoBehaviour
{
    public static ServerRequest instance;
    JsonAPIS jsonAPIS = new JsonAPIS();
    GetRoomIDJson getRoomIDInstance;
    StatisticsJsonFile statisticsInstance;
    Data itemsInstance;
    PutRequstJson requstJson;
    public int moduleID = 17;//every module has unique id
    public static int id;
    public static string roomId;
    public static string headset = "";
    [HideInInspector]
    public string startTime = "yyyy/MM/dd hh:mm:ss tt"; // use System.DateTime.Now(start time) when level start to getstart time
    [HideInInspector]
    public string endTime = "yyyy/MM/dd hh:mm:ss tt"; //use System.DateTime.Now(end time) when level end to get end time
    float score = 10;//this  is just example it can be anything 
    static bool quit;
    static bool save = false;
    // public  string json;
    private void Awake()
    {
        instance = this;
        Session.EndSession();
#if UNITY_ANDROID
        GetHeadsetSerial();
        Invoke("LoadMainMenu", 3);
#endif
        quit = false;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        startTime = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt");

#if UNITY_ANDROID
        Debug.Log("roomid");
        StartCoroutine(Get(moduleID, headset));
        Invoke("LoadMainMenu", 3); //here you load next scene after 3 seconds 
#endif


    }
    public void LoadMainMenu()
    {

        if (roomId != null)
        {

            StatisticsJsonFile.Instance.room_id = roomId;
            StatisticsJsonFile.Instance.headset = headset;
            SceneManager.LoadScene(2);
        }
        else
        {
            Debug.Log("room id is null");
            SceneManager.LoadScene(0);
        }
    }

    public void GetHeadsetSerial()
    {
        //getting serial number of hedset
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
        AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
        string AndroidIdLowerCase = secure.CallStatic<string>("getString", contentResolver, "android_id");
        string Android_ID = AndroidIdLowerCase.ToUpper();
        headset = Android_ID;
        Debug.Log(headset);
#if UNITY_ANDROID
        GetSerialRpc(headset);
#endif
    }
    public void GetSerialRpc(string serial)
    {
        headset = serial;
        PlayerPrefs.SetString("serial", serial);
        PlayerPrefs.Save();
        headset = PlayerPrefs.GetString("serial");
    }
    IEnumerator Get(int moduleId, string serial)
    {
        yield return StartCoroutine(jsonAPIS.GetRequestJson(moduleId, serial));
        getRoomIDInstance = jsonAPIS.ReturnGetRoomID();
        roomId = getRoomIDInstance.room_id;
        Debug.Log("roomID: " + roomId);
        Session.StartSession(getRoomIDInstance);
        PutRequest.instance.SetValues(Session.GetData().id, headset);
        GetRpc( Session.GetData().id, Session.GetData().room_id);//or Use getRoomIDInstance.id,getRoomIDInstance.room_id

    }
    public void GetRpc(int getId, string getRoom)
    {
        id = getId;
        roomId = getRoom;
    }
    public void SendPostRequest()
    {
        StartCoroutine(Post());
    }
    public IEnumerator Post()
    {
        yield return StartCoroutine(jsonAPIS.PostJsonItems());
        statisticsInstance = jsonAPIS.ReturnSendStatistics();
        Session.SetStats(statisticsInstance);
    }
    public void SendPutRequest()
    {
        StartCoroutine(Put(id, System.DateTime.Now.ToString(endTime), headset));
    }
    IEnumerator Put(int id, string endedAt, string serial)
    {
        Debug.Log("exit");
        if (save == false)
        {
            yield return StartCoroutine(jsonAPIS.SendPutRequest(id, endedAt, serial));
            requstJson = jsonAPIS.ReturnPutRequstJson();
            Session.SetPut(requstJson);
            save = true;
            Invoke("Exit", 1.5f);
            Debug.Log("put");
        }
    }
    private void OnApplicationQuit()
    {
        if (quit == false)
        {

            ExitRpc();

            Application.CancelQuit();
        }
        if (quit == true)
            Application.Quit();
    }
#if UNITY_ANDROID
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            ExitRpc();

        }
    }
#endif

    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            ExitRpc();
        }

    }
    //public void OnDestroy()
    //{
    //    if (quit != true)

    //        ExitRpc();
    //}
    public void ExitRpc()
    {
        Debug.Log("putExit");
        SendPutRequest();
    }

    public void Exit()
    {
        Session.EndSession();
        save = true;
        //  Destroy(this);
        Debug.Log("quit");
        quit = true;
        Application.Quit();

    }
}
