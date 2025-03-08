using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ClosedTimesValues : MonoBehaviour
{
    [SerializeField] int livel1ClosedTimeValue;
    [SerializeField] int livel3ClosedTimeValue;
    [SerializeField] MenuController menueControllerInstance;
    private void OnEnable()
    {
        if (Statistics.instance.level==3) 
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = livel3ClosedTimeValue.ToString();
        }
        else
        {
             gameObject.GetComponentInChildren<TextMeshProUGUI>().text = livel1ClosedTimeValue.ToString();
        }     
    
    }
    public void OnButtonClicked()
    {
        try
        {
        int value =Int32.Parse(gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
        menueControllerInstance.SetClosedTimeValue(value);
        }
        catch (Exception e)
        {
           Console.WriteLine("Error Setting the closed time value"+e.Message);
        }
    }
}
