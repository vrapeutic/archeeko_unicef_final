using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRemainningPrizes : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Text>().text = Statistics.instance.remainingPrizes.ToString();
    }
}
