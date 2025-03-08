using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeunuSequence : MonoBehaviour
{
    [SerializeField] GameObject[] panels;
    int currentUIIndex;

    private void Start()
    {
        currentUIIndex = 0;
        if (Statistics.instance)
            Statistics.instance.OnStartFunc();
        ShowUI();
    }

    public void NextUI()
    {
        currentUIIndex++;
        ShowUI();
    }

    public void PreviousUI()
    {
        currentUIIndex--;
        if (Statistics.instance.enviroment == 3 && currentUIIndex == 2)
        {
            currentUIIndex--;
            Debug.Log("skip day and night");
        }
        ShowUI();
    }

    void ShowUI()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == currentUIIndex)
            {
                panels[currentUIIndex].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
            }
        }
    }
}
