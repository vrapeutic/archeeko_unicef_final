using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] IntVariable level;
    //times when we can start aiming or end for one target
    List<System.DateTime> startingTimes = new List<System.DateTime>();
    List<System.DateTime> endingTimes = new List<System.DateTime>();
    //ending time -starting time
    List<double> interruptionDurations = new List<double>();
    //for following Distractors
    List<string> DistractorsName = new List<string>();
    List<double> TimeFollowingDistractors = new List<double>();
    System.DateTime registerDistractorTime;
    [SerializeField] FloatVariable timeFollowingSelectiveDistractor;
    string collectedData="";
    [SerializeField] StringVariable fileName;
    private void Start()
    {
        Debug.Log(System.DateTime.Now.ToString());
        RegisteringStartTimeForTargeting();
        //for test on mobile
       // StartCoroutine(testForMobile());

    }
    IEnumerator testForMobile()
    {
        fileName.Value = "testFormobile";
        yield return new WaitForSeconds(10);
        WriteCSV();
    }
    public void RegisteringStartTimeForTargeting()
    {
        if (!Player.instance.canPlay) return;
        startingTimes.Add(System.DateTime.Now);
        Debug.Log("StartTimeForTargeting :" + startingTimes[startingTimes.Count-1]);
    }

    public void RegisteringEndTimeForTargeting()
    {
        endingTimes.Add(System.DateTime.Now);
        Debug.Log("EndTimeForTargeting :" + endingTimes[endingTimes.Count - 1]);
    }

    public void RegisteringInterraptions()
    {
        for (int i = 0; i < endingTimes.Count; i++)
        {
            interruptionDurations.Add((endingTimes[i] - startingTimes[i]).TotalSeconds);
            Debug.Log("Interraptions " +(interruptionDurations.Count - 1)+":" + interruptionDurations[interruptionDurations.Count - 1]);
        }
    }

    //if we are selective we will optain the distracting time from camera hitted the distractor
    //if we are adaptive we will optain the distracting time from difference between raise distractor event and complation of distractor
    public void RegisteringDistractorName(string name)
    {
        if ((level.Value == 4 || level.Value == 5 || level.Value == 6) && (name == "Visitors_waving_adaptive")) return;
        DistractorsName.Add(name);
        registerDistractorTime = System.DateTime.Now;
        if (name == "Bird_passing" || name == "Visitors_waving_selective" || name == "Tractor_passing")
        {
            StartCoroutine(RegisteringDistractorFollowingTimeIenum());
        }
    }
    IEnumerator RegisteringDistractorFollowingTimeIenum()
    {
        yield return new WaitForSeconds(20);
        TimeFollowingDistractors.Add(timeFollowingSelectiveDistractor.Value);
        Debug.Log("DistractorFollowingTime " + DistractorsName[TimeFollowingDistractors.Count - 1] + ":" + TimeFollowingDistractors[TimeFollowingDistractors.Count - 1]);
    }

    public void RegisteringDistractorFollowingTime()
    {
        TimeFollowingDistractors.Add((System.DateTime.Now- registerDistractorTime).TotalSeconds);
        Debug.Log("DistractorFollowingTime " + DistractorsName[TimeFollowingDistractors.Count - 1] + ":" + TimeFollowingDistractors[TimeFollowingDistractors.Count - 1]);

    }

    public void WriteCSV()
    {
        collectedData += "Archeeko" +", "+ level.Value+Environment.NewLine;
        Debug.Log("!!!collectedData1 :"+collectedData);
        collectedData += "Target Starting Time" + ", " + "Target Hitting Time "+", "+ "Interruption Durations"+", "+
            "Distractor Name          " + ", " + "Time Following It" + Environment.NewLine;
        Debug.Log("!!!collectedData2 :" + collectedData);
        int arrLength = endingTimes.Count > TimeFollowingDistractors.Count ? endingTimes.Count : TimeFollowingDistractors.Count;
        Debug.Log("!!!arrLength: " + arrLength + " DistractorsName.Count "+ DistractorsName.Count+ " endingTimes.Count "+ endingTimes.Count);
        for (int i = 0; i < arrLength; i++)
        {
            if (i < endingTimes.Count && i < TimeFollowingDistractors.Count)
                collectedData += startingTimes[i].ToString() + ", " + endingTimes[i].ToString() + ", " + interruptionDurations[i].ToString()+", "+
                    DistractorsName[i].ToFixedString(25, ' ') + ", " + TimeFollowingDistractors[i].ToString()+Environment.NewLine;
            else if (i < endingTimes.Count) collectedData += startingTimes[i].ToString() + ", " + endingTimes[i].ToString() + ", " + interruptionDurations[i].ToString() + Environment.NewLine;
            else if (i < TimeFollowingDistractors.Count) collectedData += " , , , " + DistractorsName[i].ToFixedString(25, ' ') + ", " + TimeFollowingDistractors[i].ToString() + Environment.NewLine;
        }
        Debug.Log("!!!collectedData3 :" + collectedData);
        CSVWriter csv = new CSVWriter();
        GetComponent<CSVWriter>().WriteCSV(collectedData , fileName.Value);
        Debug.Log("!!WriteCSV");
    }


}
