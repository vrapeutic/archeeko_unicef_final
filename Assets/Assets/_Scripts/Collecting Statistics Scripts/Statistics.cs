using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Statistics : MonoBehaviour
{
    public static Statistics instance;

    public int languageIndex = 1;// 0 --> Arabic, 1 --> English, 2 --> Suadi
    
    public int character = 0;// 0 --> Hussien, 1 --> Reem
    //  [HideInInspector]
    public bool android = false;
    // [HideInInspector]
    public int level = 3;
    //[HideInInspector]
    public bool isClosedTime = false, isDay = true;// true --> Day, false --> Night

    public int enviroment; //1 is tree enviroment ,2 is garden Enviroment,3 is playroom enviroment
    //[HideInInspector]
    public bool isArchery = true; //true --> Archer, false --> Gun

    [HideInInspector]
    public int closedTimeValue = 60;
    [HideInInspector]
    public int score = 0;
    [HideInInspector]
    public int lives = 3;
    [HideInInspector]
    public int intialTries = 20;
    //[HideInInspector]
    public int tries;
    //[HideInInspector]
    public float targetDepth = 5;
    //  [HideInInspector]
    public int remainingPrizes = 10;
    [HideInInspector]
    public bool firstEnterMainMenu = true;
    [HideInInspector]
    public float focusedTime = 0.0f;

    

    #region response time
    float lastResponseTime = 0;
    int responseTimePeriodsNo = 0;
    [HideInInspector]
    public bool responeTimeBool = false;
    [HideInInspector]
    public float responeTimeCounter;
    [HideInInspector]
    public bool RocketresponeTimeBool = false;
    [HideInInspector]
    public float ballResponseTimeCounter;
    #endregion

    #region control variables
    [HideInInspector]
    public bool prizeHited = false;//to check if price hitted at last bow hitted
    [HideInInspector]
    public bool playerHited = false;
    [HideInInspector]
    public bool canPlay = true;
    #endregion

    [SerializeField] float typicalTime = 60.0f;
    [SerializeField] float tas = 55.0f;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
#if UNITY_ANDROID
        android = true;
        Debug.Log("True");
#endif
        DontDestroyOnLoad(this.gameObject);
    }
    /*    
    void Start()
    {
            lives = 3;
            score = 0;
            tries = 20;
            remainingPrizes = 10;
            prizeHited = false;//to check if price hitted at last bow hitted
            playerHited = false;
            StatisticsJsonFile.Instance.data.attempt_start_time = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt");
            //Debug.Log("no, instance that have do't destroy on the load play start function");
            responeTimeBool = false;
            responeTimeCounter = 0.0f;
            RocketresponeTimeBool = false;
            ballResponseTimeCounter = 0.0f;
            focusedTime = 0.0f;    
    }
    */
    public void OnStartFunc()
    {
        lives = 3;
        score = 0;
      //  tries = 20;
        remainingPrizes = 10;
        prizeHited = false;//to check if price hitted at last bow hitted
        playerHited = false;
        StatisticsJsonFile.Instance.data.attempt_start_time = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt");
        //Debug.Log("no, instance that have do't destroy on the load play start function");
        responeTimeBool = false;
        responeTimeCounter = 0.0f;
        RocketresponeTimeBool = false;
        ballResponseTimeCounter = 0.0f;
        focusedTime = 0.0f;
    }

    #region implusivity score
    void CaculateImplusivityScore()
    {
        float tar = CalculateTAR();//target ratio
        float tir = CalculateTIR();
        float implusivityScore = (float)(1 / ((-tar) * ((Mathf.Log10(tir) - 1 + Mathf.Epsilon))));
        if (tar == 0) implusivityScore = 0;
        StatisticsJsonFile.Instance.data.impulsivity_score = implusivityScore;
        Debug.Log("tir : " + tir + "time taken : " + StatisticsJsonFile.Instance.data.actual_duration_in_seconds + ",typicalTime: " + typicalTime);
        Debug.Log("Tar : " + tar + ", targets hit : " + (10 - remainingPrizes));// +  ", total trials: " + consumedArches);
    }

    float CalculateTAR()
    {
        return (float)(10 - remainingPrizes) / 10; ;
    }
    float CalculateTIR()
    {
        float currentTime = StatisticsJsonFile.Instance.data.actual_duration_in_seconds = Time.timeSinceLevelLoad;
        if (Statistics.instance.level == 3) typicalTime = typicalTime * 2;
        return (float)currentTime / (float)typicalTime;//time ratio
    }
    void CalculateImplusivityScoreWithAiming()
    {
        float aimingScore = CalculateAimingScore();//target ratio
        float tir = CalculateTIR();
        float implusivityScoreWithAiming = (float)(1 / ((-aimingScore) * ((Mathf.Log10(tir) - 1 + Mathf.Epsilon))));
        if (aimingScore == 0) implusivityScoreWithAiming = 0;
        if (aimingScore != aimingScore) implusivityScoreWithAiming = 0;//to check nan
        StatisticsJsonFile.Instance.data.impulsivity_score_with_aiming = implusivityScoreWithAiming;
    }
    float CalculateAimingScore()
    {
        int successArches = 10 - Statistics.instance.remainingPrizes;
        //Debug.Log("successArches :" + successArches);
        int consumedArches = Statistics.instance.intialTries - Statistics.instance.tries;
        //Debug.Log("consumedArches :" + consumedArches);
        float aimingScore = (float)successArches / (float)consumedArches;
        if (Statistics.instance.level == 3)
        {
            int lostLives = 3 - instance.lives;
            aimingScore = ((float)successArches - ((float)lostLives / 2)) / (float)consumedArches;
        }
        StatisticsJsonFile.Instance.data.success_arches_count = successArches;
        StatisticsJsonFile.Instance.data.consumed_arches = consumedArches;
        Debug.Log("aiming score : " + aimingScore + ", targets hit : " + (10 - instance.remainingPrizes));// +  ", total trials: " + consumedArches);
        return aimingScore;
    }
    #endregion

    #region response time
    void CalculateResponseTime()
    {
        float responseTime = (float)lastResponseTime / (float)responseTimePeriodsNo;
        if (Statistics.instance.level == 3) responseTime = (float)(responseTime * .7 + ((float)ballResponseTimeCounter / (10 - Statistics.instance.remainingPrizes)) * .3);
        if (responeTimeCounter == 0) responseTime = 0;
        StatisticsJsonFile.Instance.data.response_time = responseTime;
        Debug.Log("finial responce time : " + responseTime + " turns : " + responseTimePeriodsNo + " responeTimeCounter : "
                + responeTimeCounter + " ballResponseTimeCounter : " + ballResponseTimeCounter);
    }
    #endregion

    # region ommision score
    void CalculateOmissionScore()
    {
        if (Statistics.instance.level == 3) tas = tas * 2;
        float aas = focusedTime;
        float tfd = Time.timeSinceLevelLoad - focusedTime;
        float omisionScore = (float)((float)tas / (aas + Mathf.Epsilon));
        float distractionEnduranceScore = 1 - ((float)tfd / (float)tas);
        if (Statistics.instance.level == 1) distractionEnduranceScore = 0.0f;
        StatisticsJsonFile.Instance.data.omission_score = omisionScore;
        StatisticsJsonFile.Instance.data.distraction_endurance_score = distractionEnduranceScore;
        Debug.Log("omisionScore: " + omisionScore + "aas : " + aas + " ,tas : " + tas + " ,tfd : " + tfd);
    }
    #endregion

    void AssignRemainingStats()
    {
        if (Statistics.instance.isClosedTime) StatisticsJsonFile.Instance.data.attempt_type = "closed time";
        else StatisticsJsonFile.Instance.data.attempt_type = "Opened time";

        StatisticsJsonFile.Instance.data.distance = Statistics.instance.targetDepth;

        StatisticsJsonFile.Instance.data.total_arches_count = Statistics.instance.intialTries;

        StatisticsJsonFile.Instance.data.level = Statistics.instance.level.ToString();

        StatisticsJsonFile.Instance.data.remaining_arches = Statistics.instance.tries;

        StatisticsJsonFile.Instance.data.total_prizes = 10;

        StatisticsJsonFile.Instance.data.remaining_prizes = Statistics.instance.remainingPrizes;

        StatisticsJsonFile.Instance.data.attempt_end_time = System.DateTime.Now.ToString();

        StatisticsJsonFile.Instance.data.actual_attention_time = focusedTime;
    }
    public void SendAttemptStatistics()
    {
        CaculateImplusivityScore();
        CalculateImplusivityScoreWithAiming();
        CalculateResponseTime();
        CalculateOmissionScore();
        AssignRemainingStats();
        try
        {
         ServerRequest.instance.SendPostRequest();

        }
        catch (System.Exception)
        {

            Debug.Log("can`t call ServerRequest.instance.SendPostRequest(); ");
        }
    }
    private void Update()
    {
        if (responeTimeBool)
        {
            responeTimeCounter += Time.deltaTime;
            // if (responeTimeCounter % 1 < 0.02) Debug.Log("responeTimeCounter: " + responeTimeCounter);
        }
        /**/
        if (RocketresponeTimeBool)
        {
            ballResponseTimeCounter += Time.deltaTime;
            // if (ballResponseTimeCounter % 1 < 0.02) Debug.Log("ballResponseTimeCounter: " + ballResponseTimeCounter);
        }
        /**/
    }
    public void StopResponseTimeCounter()
    {
        responeTimeBool = false;
        Debug.Log("Response Time : response time counter : " + (responeTimeCounter - lastResponseTime - 1));
        if ((responeTimeCounter - lastResponseTime - 1) > .1) responseTimePeriodsNo++;
        lastResponseTime = responeTimeCounter - responseTimePeriodsNo;
    }
}