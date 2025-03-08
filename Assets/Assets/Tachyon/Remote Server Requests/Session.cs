using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session : MonoBehaviour
{
    static GetRoomIDJson getData = null;
    static StatisticsJsonFile getStatistics = null;
    static Data getItems = null;
    static PutRequstJson getRequstJson = null;
    public static void StartSession(GetRoomIDJson roomIDJson)
    {
        getData = roomIDJson;

    }
    public static GetRoomIDJson GetData()
    {
        return getData;

    }
    public static void SetStats(StatisticsJsonFile jsonFile)
    {
        getStatistics = jsonFile;
    }
    public static StatisticsJsonFile GetStatistics()
    {
        return getStatistics;
    }
    public static void SetPut(PutRequstJson ReturnPutRequstJson)
    {
        getRequstJson = ReturnPutRequstJson;
    }
    public static PutRequstJson AfterPut()
    {
        return getRequstJson;
    }
    public static void EndSession()
    {
        getData = null;
        getStatistics = null;
        getRequstJson = null;
    }
}
