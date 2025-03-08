using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCalculator : MonoBehaviour
{
    [SerializeField] IntVariable level;
    [SerializeField] StringVariable typeOfAttention;
    [SerializeField] IntVariable noOfDistractors;
    [SerializeField] IntVariable sustainedValue;
    // Start is called before the first frame update
    void Start()
    {
        if (typeOfAttention.Value == "sustained")
        {
            if (sustainedValue.Value == 20) level.Value=1;
            else if (sustainedValue.Value == 40) level.Value = 2;
            else if (sustainedValue.Value == 60) level.Value = 3;
        }
        else if (typeOfAttention.Value == "selective")
        {
            if(noOfDistractors.Value==1) level.Value = 4;
            else if (noOfDistractors.Value == 2) level.Value = 5;
            else if (noOfDistractors.Value == 3) level.Value = 6;
        }

        else if (typeOfAttention.Value == "adaptive")
        {
            if (noOfDistractors.Value == 1) level.Value = 7;
            else if (noOfDistractors.Value == 2) level.Value = 8;
            else if (noOfDistractors.Value == 3) level.Value = 9;
        }
    }
}
