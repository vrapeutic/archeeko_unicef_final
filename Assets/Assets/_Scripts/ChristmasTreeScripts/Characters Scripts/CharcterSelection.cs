using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcterSelection : MonoBehaviour
{
    private void Start()
    {
        if (Statistics.instance.languageIndex == 0)
        {
            if (Statistics.instance.character == 0)
            {
                if (gameObject.tag != "HussienAR")
                    Destroy(gameObject);
            }
            else if (Statistics.instance.character == 1)
            {
                if (gameObject.tag != "ReemAR")
                    Destroy(gameObject);
            }
        }
        else if (Statistics.instance.languageIndex == 1)
        {
            if (Statistics.instance.character == 0)
            {
                if (gameObject.tag != "HussienENG")
                    Destroy(gameObject);
            }
            else if (Statistics.instance.character == 1)
            {
                if (gameObject.tag != "ReemENG")
                    Destroy(gameObject);
            }
        }
        else if (Statistics.instance.languageIndex == 2)
        {
            if (Statistics.instance.character == 0)
            {
                if (gameObject.tag != "HussienSU")
                    Destroy(gameObject);
            }
            else if (Statistics.instance.character == 1)
            {
                if (gameObject.tag != "ReemSU")
                    Destroy(gameObject);
            }
        }

        else if (Statistics.instance.languageIndex == 3)
        {
            if (Statistics.instance.character == 0)
            {
                if (gameObject.tag != "HussienVIT")
                    Destroy(gameObject);
            }
            else if (Statistics.instance.character == 1)
            {
                if (gameObject.tag != "ReemVIT")
                    Destroy(gameObject);
            }
        }
    }

}
