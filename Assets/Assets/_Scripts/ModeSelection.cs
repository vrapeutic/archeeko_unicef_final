using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Statistics.instance.isDay == true)
        {
            RenderSettings.fog = false;
            if (gameObject.tag != "DayMode")
                Destroy(gameObject);
        }
        else if (Statistics.instance.isDay == false)
        {
            RenderSettings.fog = true;
            if (gameObject.tag != "NightMode")
                Destroy(gameObject);
        }
    }  
}
