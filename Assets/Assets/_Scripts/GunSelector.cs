using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Statistics.instance.isArchery == true)
        {
            if (gameObject.tag != "ArcheryBow")
                Destroy(gameObject);
        }
        else if (Statistics.instance.isArchery == false)
        {
            if (gameObject.tag != "PaintGun")
                Destroy(gameObject);
        }
    }

}
