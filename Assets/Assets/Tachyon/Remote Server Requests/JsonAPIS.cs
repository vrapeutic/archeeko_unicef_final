using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JsonAPIS : MonoBehaviour
{
    GetRoomIDJson iDJsonInstance;
    Data itemsInstance;
    StatisticsJsonFile statisticsInstance;
    PutRequstJson putRequstJsonInstance;
    #region GetRequest

    public IEnumerator GetRequestJson(int id, string serial)
    {
        WWWForm form = new WWWForm();
        UnityWebRequest www = UnityWebRequest.Get("https://dashboard.myvrapeutic.com/api/v1/module_sessions/find_room?headset=" + serial + "&" + "vr_module_id=" + id.ToString());
        //   www.chunkedTransfer = false;
        yield return www.Send();
        Debug.Log("https://dashboard.myvrapeutic.com/api/v1/module_sessions/find_room?headset=" + serial + "&" + "vr_module_id=" + id.ToString());
        iDJsonInstance = JsonUtility.FromJson<GetRoomIDJson>(www.downloadHandler.text);
        Debug.Log(iDJsonInstance);
        if (www.error != null)
        {
            Debug.Log("error" + www.error);
        }
        else if (www.downloadHandler.text != null)
        {
            Debug.Log("all" + www.downloadHandler.text);
        }

    }
    public GetRoomIDJson ReturnGetRoomID()
    {
        return iDJsonInstance;
    }
    #endregion
    
    #region PostRequest
    public IEnumerator PostJsonItems()
    {
        string json = ConvertPostToJson();
        Debug.Log(json);
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(json);
        string url = "https://dashboard.myvrapeutic.com/api/v1/statistics";
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        Debug.Log("SendjsonIEnum :" + json);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        statisticsInstance = JsonUtility.FromJson<StatisticsJsonFile>(www.downloadHandler.text);
        Debug.Log("Send request Resonce code: " + www.responseCode);
        if (www.error != null)
        {
            Debug.Log("error" + www.error);
        }
        else if (www.downloadHandler.text != null)
        {

            Debug.Log("all" + www.downloadHandler.text);
        }

    }
    string ConvertPostToJson()
    {
        ////StatisticsJsonFile postRequest = new StatisticsJsonFile();
        //StatisticsJsonFile.Instance.headset = "";
        //StatisticsJsonFile.Instance.room_id = "";
        //StatisticsJsonFile.Instance.data.start_time = "";
        //StatisticsJsonFile.Instance.data.end_time = "";
        //StatisticsJsonFile.Instance.data.score = 10;
        return JsonUtility.ToJson(StatisticsJsonFile.Instance);
    }
    public StatisticsJsonFile ReturnSendStatistics()
    {
        return statisticsInstance;
    }
    #endregion

    #region PutRequest
    public IEnumerator SendPutRequest(int id, string endedAt, string serial)
    {
        string json = ConvertPutTojson(id, System.DateTime.Now.ToString(endedAt), serial);
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(json);
        string url = "https://dashboard.myvrapeutic.com/api/v1/module_sessions";
        string putUrl = url + "/" + id + "?" + "ended_at=" + System.DateTime.Now.ToString(endedAt) + "&" + "headset=" + serial;
        UnityWebRequest www = UnityWebRequest.Put(putUrl, postData);
        yield return www.SendWebRequest();
        Debug.Log(www.responseCode);
        putRequstJsonInstance = JsonUtility.FromJson<PutRequstJson>(www.downloadHandler.text);
        Debug.Log(putRequstJsonInstance);
        if (www.error != null)
        {
            Debug.Log("error" + www.error);
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
    public PutRequstJson ReturnPutRequstJson()
    {
        return putRequstJsonInstance;
    }
    #endregion
}
