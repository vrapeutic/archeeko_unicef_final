using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LanguageSelection : MonoBehaviour
{
    private void Awake()
    {
        try
        {
            if (!Statistics.instance.firstEnterMainMenu)
            {
            Destroy(gameObject);
             }
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to access Statistics to get the firstEnterMainMenu"+e.Message);
        }
    }


    public void OnLanguageSelect(int languageNO)
    {
        OnLanguageSelectRPC( languageNO);
        Statistics.instance.firstEnterMainMenu = false;
        
    }

    public void OnLanguageSelectRPC(int languageNO)
    {
         Statistics.instance.languageIndex = languageNO;
    }
}

