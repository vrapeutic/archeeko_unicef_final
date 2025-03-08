using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class PutRequest : MonoBehaviour
{

    public static PutRequest instance;
    JsonAPIS jsonApiInstance = new JsonAPIS();
    int id;
    string timeFormat = "yyyy/MM/dd hh:mm:ss tt";
    string headset = "";
    PutRequstJson putRequstJsonInstance = new PutRequstJson();

    bool firstPut = true;
//#if UNITY_ANDROID
//    async void OnDestroy()
//    {
//        await SendPutRequest();
//    }
//#endif

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetValues(int _id ,string _serial)
    {
        id =_id;
        headset = _serial;      
    }

    public void SendPutRequest()
    {
        firstPut = true;
        StartCoroutine(SendPutRequestIenum());
    }

    IEnumerator SendPutRequestIenum()
    {
        string json = ConvertPutTojson(PutRequest.instance.id, System.DateTime.Now.ToString(timeFormat), PutRequest.instance.headset);
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(json);
        string url = "https://dashboard.myvrapeutic.com/api/v1/module_sessions";
        string putUrl = url + "/" + PutRequest.instance.id + "?" + "ended_at=" + System.DateTime.Now.ToString(timeFormat) + "&" + "headset=" + PutRequest.instance.headset;
        UnityWebRequest www = UnityWebRequest.Put(putUrl, postData);
        yield return www.SendWebRequest();
        Debug.Log("put request responce code: "+www.responseCode);
        putRequstJsonInstance = JsonUtility.FromJson<PutRequstJson>(www.downloadHandler.text);
        Debug.Log(putRequstJsonInstance);
        if (www.error != null)
        {
            Debug.Log("error" + www.error);
            if (firstPut)
            {
                StartCoroutine(SendPutRequestIenum());
                firstPut = false;
            }
        }
        else if (www.downloadHandler.text != null)
        {
            Debug.Log("all" + www.downloadHandler.text);
        }
    }

    string ConvertPutTojson(int id, string endedAt, string serial)
    {
        PutRequstJson putRequst = new PutRequstJson();
        putRequst.id = id;
        putRequst.ended_at = endedAt;
        putRequst.headset = serial;
        return JsonUtility.ToJson(putRequst);
    }
}
