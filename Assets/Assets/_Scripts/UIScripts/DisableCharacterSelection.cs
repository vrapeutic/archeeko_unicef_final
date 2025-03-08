using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCharacterSelection : MonoBehaviour
{
    MeunuSequence menuSequence;
    void Awake()
    {
        menuSequence = FindObjectOfType<MeunuSequence>();
    }

    private void OnEnable()
    {
        if (Statistics.instance.enviroment != 1)
        {
            menuSequence.NextUI();
        }
    }

}
