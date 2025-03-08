using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;
using TMPro;
public class CsvReadWrite : MonoBehaviour
{
    string FileName = "";
    TextWriter tw;

    // Use this for initialization
    void Start()
    {
        FileName = Application.dataPath + "/CSVData.csv";
        tw= new StreamWriter(FileName, true);
        //tw.WriteLine("Game Name, Correct Attempts, Wrong Attempts, Level, Right Hand Counter, Left Hand Counter, Start Time, End Time, Unique Code");
    }

    public void WriteCSV()
    {
        tw.Close();
        tw = new StreamWriter(FileName, true);

        tw.WriteLine(SceneManager.GetActiveScene().name + ", " + "stats.correctAttempts" +
        ", " + "stats.wrongAttempts" + ", " + "stats.level" + ", " + "stats.RightHandHitCounter" +
        ", " + "stats.LeftHandHitCounter" ); 

        tw.Close();
    }
}
