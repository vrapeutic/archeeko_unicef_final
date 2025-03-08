using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonTargetMovement : MonoBehaviour
{

    Transform [] nodes;
    [SerializeField]
    GameObject player;
    Vector3 startPosition;
    Quaternion startRotation;
    Coroutine MoveDistructorCoroutine;
    //start postion for maze
    [SerializeField]
    float speed;
    int curretNode;
    float timer;
    static Vector3 currentPositionHolder;
    static Quaternion currentRotationHolder;
    //hold node postion
    // Use this for initialization

    private void Start()
    {
        // Return it to awake
        if (Statistics.instance)
          if (Statistics.instance.level == 1) Destroy(this.transform.parent.gameObject);
        nodes = GetComponentsInChildren<Transform>();
        CheckNodes();
         
        StartMovement();
        if (Statistics.instance)
        if(Statistics.instance.android) StartCoroutine(Wait5Seconds());
    }
    public void StartMovement()
    {
        
        if (MoveDistructorCoroutine != null)
            StopCoroutine(MoveDistructorCoroutine);
        MoveDistructorCoroutine = StartCoroutine(Movement());
    }


    //check held current node to current postion Holder
    private void CheckNodes()
    {

        timer = 0;
        startPosition = player.transform.position;
        startRotation = player.transform.rotation;
        currentPositionHolder = nodes[curretNode].position;
        currentRotationHolder = nodes[curretNode].rotation;


    }

    IEnumerator Movement()
    {
        while (true)
        {           
                //Debug.Log(curretNode);
                timer += Time.deltaTime * speed;
                // this make the path moves
                if (player.transform.position != currentPositionHolder)
                {
                    //move player to the node
                    player.transform.position = Vector3.Lerp(startPosition, currentPositionHolder, timer);
                     player.transform.rotation = Quaternion.Lerp(startRotation, currentRotationHolder, timer);
                }
                else if (curretNode < nodes.Length - 1)
                {
                    //go to next one
                    curretNode++;
                    CheckNodes();
                }
                else if (curretNode == nodes.Length - 1)
                {
                curretNode = 0;
                }
                yield return null;
        }
    }

    IEnumerator Wait5Seconds()
    {
        yield return new WaitForSeconds(5f);
        StartMovement();
    }


}
