using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject fadeObject;
    [SerializeField] GameObject playerMovementDetector;
    public bool canPlay = true;
    public static Player instance;

    public delegate void ClickAction();
    public static event ClickAction OnPlayerHitted;

    float lastResponseTimer = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        canPlay = true;
        Enemy.OnAttack += OnAttackFunc;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            GetComponent<AudioSource>().Play();
            Statistics.instance.lives--;
            Debug.Log("aiming score : lost lives : " + (3 - Statistics.instance.lives));
            Statistics.instance.playerHited = true;
            fadeObject.SetActive(true);
            if (Statistics.instance.lives <= 0)
                GameManager.instance.EndingUnSuccessful();
            OnPlayerHitted();
            Statistics.instance.RocketresponeTimeBool = false;
            Debug.Log("Response Time : rocket time counter : " + (Statistics.instance.ballResponseTimeCounter - lastResponseTimer));
            lastResponseTimer = Statistics.instance.ballResponseTimeCounter;
            playerMovementDetector.SetActive(false);
        }
        else if (other.tag == "MovementDetector")
        {
            //Debug.Log("MovementDetector Hitted");
            Statistics.instance.RocketresponeTimeBool = false;
            Debug.Log("Response Time : ball time counter : " + (Statistics.instance.ballResponseTimeCounter - lastResponseTimer));
            lastResponseTimer = Statistics.instance.ballResponseTimeCounter;
            playerMovementDetector.SetActive(false);
        }
    }

    public void CheckPlayerHealth()
    {
        if (Statistics.instance.lives == 1) StartCoroutine(CheckPlayerHitted());
        else GameManager.instance.EndingSuccessful();
    }
    IEnumerator CheckPlayerHitted()
    {
        Statistics.instance.playerHited = false;
        yield return new WaitForSeconds(6);
        if (Statistics.instance.playerHited) GameManager.instance.EndingUnSuccessful();
        else GameManager.instance.EndingSuccessful();
    }

    void OnAttackFunc()
    {
        //Debug.Log("On attack Func called");
        playerMovementDetector.SetActive(true);
        playerMovementDetector.transform.position = this.transform.position;
    }

    private void OnDisable()
    {
        OnPlayerHitted -= OnAttackFunc;
    }
}
