using System;

[Serializable]
public class GetRoomIDJson
{
    public int id;
    public string room_id;
}

[Serializable]
public class StatisticsJsonFile
{
    private static StatisticsJsonFile instance = null;
    private static readonly object padlock = new object();

    StatisticsJsonFile()
    {
    }
    public static StatisticsJsonFile Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new StatisticsJsonFile();
                }
                return instance;
            }
        }
    }
    public string headset;
    public string room_id;
    public JsonItems data = new JsonItems();
}

//this class will be customized to every module
[Serializable]
public class JsonItems
{
    //add data attributes here 
    public string session_start_time;//this for example
    public string attempt_start_time;
    public string attempt_end_time;
    public float expected_duration_in_seconds =120;
    public float actual_duration_in_seconds;
    public string level ="3";
    public string attempt_type ="open";// please don`t show this in your UI ya hossam
    public int total_arches_count=20;
    public int consumed_arches;
    public int remaining_arches;
    public int success_arches_count;
    public float distance=6;
    public float total_prizes=10;
    public int remaining_prizes;
    public float impulsivity_score;
    public float impulsivity_score_with_aiming;
    public float response_time;
    public float omission_score;
    public float distraction_endurance_score;
    public float actual_attention_time;
}

//to form json file put request
[Serializable]
public class PutRequstJson
{
    public int id;
    public string ended_at;//the current time when set put request
    public string headset;
}

//for recieving pusher json file
[Serializable]
public class PusherJson
{
    public string room_id;
    public string package;
    public string name;
}
[Serializable]
public class Data
{
    //add data attributes here 
    public string start_time;
    public string end_time;
    public float score;

}
