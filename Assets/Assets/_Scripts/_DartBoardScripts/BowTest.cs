using System;
using System.Collections;
using UnityEngine;
public class BowTest : MonoBehaviour
{
    [SerializeField] GameObject[] arrows;
    [SerializeField] GameEvent arrowFired;
    int arrowsNo;
    public int currentArrowNo = 0;
    [SerializeField] float grabThreshold = .15f;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] Transform socket;
    [SerializeField] Vector3 polingPosion;
    [SerializeField] float waitTimeForPole;
    WaitForSeconds polingTime;
    Coroutine PoleArrowRoutine;
    Transform pullingHand = null;
    StickyArrow currentArrow = null;
    Animator pullingAnimator;
    float pullingValue = 0.0f;
    bool canPlay = true;
    public delegate void ReleaseAction();
    public static event ReleaseAction OnRelease;
    public static BowTest instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    private void Start()
    {
        polingTime = new WaitForSeconds(0.4f);
        try
        {
            pullingAnimator = GetComponent<Animator>();
        }
        catch (Exception e)
        {
            Console.WriteLine("couldn't access the bow animator" + e.Message);
        }
        arrowsNo = arrows.Length;
        for (int i = 0; i < arrowsNo; i++)
        {
            arrows[i].SetActive(false);
        }
        PoleArrowRoutine = StartCoroutine(PoleArrow());
    }

    private void Update()
    {
        UpdateBowAnimation();
    }
    #region calculate pulling value and Update Bow animation
    public void UpdateBowAnimation() // it keeps the pulling value updated
    {
        if (!pullingHand || !currentArrow || !canPlay)
        {
            return;
        }
        pullingValue = CalculatePull(pullingHand);
        pullingValue = Mathf.Clamp(pullingValue, 0.0f, 1.0f);
        pullingAnimator.SetFloat("Blend", pullingValue);
    }

    public float CalculatePull(Transform pullingHand)
    {
        Vector3 direction = endPoint.position - startPoint.position;
        float magnitude = direction.magnitude;
        direction.Normalize();
        Vector3 handDirection = pullingHand.position - startPoint.position;
        float pull = Vector3.Dot(handDirection, direction) / magnitude;
        return pull;
    }
    #endregion
    public IEnumerator PoleArrow()
    {
        yield return polingTime;
        currentArrow = arrows[currentArrowNo].GetComponent<StickyArrow>();
        arrows[currentArrowNo].SetActive(true);
        arrows[currentArrowNo].GetComponent<Rigidbody>().isKinematic = true;
        arrows[currentArrowNo].GetComponent<Rigidbody>().useGravity = false;
        arrows[currentArrowNo].transform.SetParent(socket);
        arrows[currentArrowNo].transform.localPosition = polingPosion;
        arrows[currentArrowNo].transform.localEulerAngles = Vector3.zero;

        try
        {
            if (Statistics.instance.android)
            {
                try
                {
                    if (Statistics.instance.tries == 1)
                    {
                        arrows[currentArrowNo].AddComponent<LastProjectile>();
                        Debug.Log("last arrow component is added");
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine("couldn't access statistics to get current tries" + e.Message);
                }
                //Debug.Log("currentArrowNo" + currentArrowNo);
                //currentArrowNo = currentArrowNo % arrowsNo;

            }
        }
        catch (Exception e)
        {
            Console.WriteLine("couldn't access statistics" + e.Message);
        }
        //currentArrowNo++;
    }

    public void Pull(Transform hand)
    {
        if (!canPlay) return;
        float distance = Vector3.Distance(hand.position, startPoint.position);
        if (distance > grabThreshold) return;
        pullingHand = hand;
    }


    public void Release()
    {
        if (pullingValue > .25f&&canPlay)
        {
            if (Statistics.instance.android)
            {
                ReleaseArrow();
            }
        }

    }

    public void ReleaseArrow()
    {
        currentArrowNo++;
        Debug.Log("release is working just fine" + currentArrowNo);
        currentArrow.Fire(pullingValue);
        currentArrow.GetComponent<AudioSource>().Play();
        currentArrow = null;
        pullingHand = null;
        pullingValue = 0;
        pullingAnimator.SetFloat("Blend", 0);

            Statistics.instance.tries--;

        Debug.Log("consumed arches : " +(Statistics.instance.intialTries - Statistics.instance.tries));

            if (Statistics.instance.tries > 0 && Player.instance.canPlay)
            {
                //Pole the next arrow
                PoleArrowRoutine = StartCoroutine(PoleArrow());
            }
            else if (Statistics.instance.tries == 0)
            {
                //Debug.Log("Statistics.instance.tries == 0");
                arrows[currentArrowNo - 1].GetComponent<LastProjectile>().ReleaseLastProjectile();
                //Debug.Log("currentArrowNo - 1 "+ (currentArrowNo - 1));
            }
            Statistics.instance.StopResponseTimeCounter();
        OnRelease();
        arrowFired.Raise();
    }

    public void ResetPulling()
    {
        currentArrow = null;
        pullingHand = null;
        pullingValue = 0;
        pullingAnimator.SetFloat("Blend", 0);
    }

    public void EnablePlaying()
    {
        canPlay = true;
        if (currentArrow != null)
        {
            foreach (var rend in currentArrow.GetComponentsInChildren<MeshRenderer>())
            {
                rend.enabled = true;
            }
        }
    }

    public void DisablePlaying()
    {
        canPlay = false;
        if (currentArrow != null)
        {
            foreach (var rend in currentArrow.GetComponentsInChildren<MeshRenderer>())
            {
                rend.enabled = false;
            }
        }
    }
}