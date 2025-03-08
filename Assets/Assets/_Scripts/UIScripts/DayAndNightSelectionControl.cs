using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNightSelectionControl : MonoBehaviour
{
    MeunuSequence menuSequence;
    void Awake()
    {
        menuSequence = FindObjectOfType<MeunuSequence>();
    }

    private void OnEnable()
    {
        if (Statistics.instance.enviroment == 3) menuSequence.NextUI();
        Debug.Log("day and night panel enabled");
    }
}
