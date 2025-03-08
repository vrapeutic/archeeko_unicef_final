using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class Target : MonoBehaviour
{
    public GameObject playingRoom;
    [SerializeField] GameObject[] Gifts;
    [SerializeField] StringVariable typeOfAttention;
    [SerializeField] IntVariable sustainedValue;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Garden Game" && typeOfAttention.Value == "sustained")
        {
            if (sustainedValue.Value == 20) EnableNeededObjects(4);
            else if (sustainedValue.Value == 40) EnableNeededObjects(8);
            else if (sustainedValue.Value == 60) EnableNeededObjects(12);
        }
        else if(SceneManager.GetActiveScene().name == "Garden Game")
        {
            EnableNeededObjects(10);
        }

        if (SceneManager.GetActiveScene().name == "Archery_Room" || SceneManager.GetActiveScene().name == "PaintGun_Room")
        {
            playingRoom.transform.position = new Vector3(playingRoom.transform.position.x, playingRoom.transform.position.y, Statistics.instance.targetDepth - 5.1050f);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Statistics.instance.targetDepth);
        }

    }

    void EnableNeededObjects (int no)
    {
        for (int i = 0; i < Gifts.Length; i++)
        {
            if (i < no) Gifts[i].SetActive(true);
            else Gifts[i].SetActive(false);
        }
    }

    void Update()
    {
        if (!Statistics.instance.android) return;
     
        if (SceneManager.GetActiveScene().name == "Garden Game" || SceneManager.GetActiveScene().name == "GameNight")
        {
            if (this.gameObject.transform.GetChild(0).GetComponent<Renderer>().isVisible)
            {
                try
                {
                 Statistics.instance.focusedTime += Time.deltaTime;
                }
                catch (Exception e)
                {
                   Console.WriteLine("couldn't access statistics to update focused time" +e.Message);
                }
            }        
        }
        else
        {
            if (this.gameObject.transform.GetComponentInChildren<Renderer>().isVisible)
            {
                try
                {
                Statistics.instance.focusedTime += Time.deltaTime;
                }
                catch (Exception e)
                {
                   Console.WriteLine("couldn't access statistics to update focused time" +e.Message);
                }
            }
        }
    }
}