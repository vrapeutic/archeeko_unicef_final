using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvDistractor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     if(Statistics.instance.level==3)
     {
         GetComponent<UnityEngine.Video.VideoPlayer>().Play();
     }   
    }

}
