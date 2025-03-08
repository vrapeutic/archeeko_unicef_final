using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private void Awake()
    {
        if (Statistics.instance.level == 3)
        {
            if (gameObject.name == "Target Tree1")
            {
                //Debug.Log("tree disabled: " + gameObject.name);
                Destroy(gameObject);
            }

        }
        else
        {
            if (gameObject.name == "Target Tree3")
            {
                //Debug.Log("tree disabled: " + gameObject.name);
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,Statistics.instance.targetDepth);
    }
    private void Update()
    {
        if (!Statistics.instance.android) return;
        if (this.gameObject.transform.GetChild(0).GetComponent<Renderer>().isVisible) Statistics.instance.focusedTime += Time.deltaTime;
    }

}
