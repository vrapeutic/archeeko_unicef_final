using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bow : MonoBehaviour
{
    [Header("Assets")]
    [SerializeField] GameObject[] arrow;
    int arrowsNo;
    public int currentArrowNo = 0;

    [Header("Bow")]
    [SerializeField] float grabThreshold = .15f;

    [SerializeField] Transform startPoint;

    [SerializeField] Transform endPoint;

    [SerializeField] Transform socket;

    [SerializeField] Vector3 polingPosion; //local location where bow is placed

    [SerializeField] float waitTimeForPole;
     WaitForSeconds polingTime;
     Coroutine PoleArrowRoutine;
    Transform pullingHand = null;
    Arrow currentArrow = null;
    Animator pullingAnimator;
    float pullingValue = 0.0f;
   
    public static Bow instance;
    public delegate void ReleaseAction();
    public static event ReleaseAction OnRelease;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    private void Start()
    {
        pullingAnimator = GetComponent<Animator>();
        polingTime = new WaitForSeconds(waitTimeForPole);
        arrowsNo = arrow.Length;
        for (int i = 0; i < arrowsNo; i++)
        {
            arrow[i].SetActive(false);
        }
        PoleArrowRoutine = StartCoroutine(PoleArrow(currentArrowNo));
    }

    private void Update()
    {
        UpdateBowAnimation();
    }

#region calculate pulling value and Update Bow animation
    //we will update the value that user will pull the bow and animation on the Bow
    private void UpdateBowAnimation()
    {
        if (!pullingHand || !currentArrow) return;
        pullingValue = CalculatePull(pullingHand);
        pullingValue = Mathf.Clamp(pullingValue, 0.0f, 1.0f);
        //set Bow animation Value
        pullingAnimator.SetFloat("Blend", pullingValue);
    }
    public float CalculatePull(Transform pullingHand)
    {
        //Ideal Vector Diraction
        Vector3 direction = endPoint.position - startPoint.position;
        float magnitude = direction.magnitude;
        direction.Normalize();

        //Actual Vector Diraction
        Vector3 handDirection = pullingHand.position - startPoint.position;
        //we use dot product to see the similarity
       //between the Ideal Vector Diraction and Actual Vector Diraction
        float pull = Vector3.Dot(handDirection, direction) / magnitude;
        //the highest similarity the highest value
        return pull;
    }
#endregion

    //when user
    public IEnumerator PoleArrow(int _currentArrowNo)
    {
        yield return polingTime;
        arrow[_currentArrowNo].SetActive(true);
        arrow[_currentArrowNo].GetComponent<Rigidbody>().isKinematic = true;
        arrow[_currentArrowNo].GetComponent<Rigidbody>().useGravity = false;
        arrow[_currentArrowNo].transform.SetParent(socket);
        arrow[_currentArrowNo].transform.localPosition = polingPosion;
        arrow[_currentArrowNo].transform.localEulerAngles = Vector3.zero;

        //set
        if (Statistics.instance.android)
        {
            currentArrow = arrow[_currentArrowNo].GetComponent<Arrow>();
            if (Statistics.instance.tries == 1)
            {
                arrow[_currentArrowNo].AddComponent<LastProjectile>();
                //Debug.Log("last arrow component is added");
            }
            //Debug.Log("currentArrowNo"+ currentArrowNo);
            currentArrowNo++;
            //currentArrowNo = currentArrowNo % arrowsNo;
        }
    }
    public void Pull(Transform hand)
    {
        //calculate distance between current hand position and the start Pulling Position
        float distance = Vector3.Distance(hand.position, startPoint.position);
        if (distance > grabThreshold) return;
        pullingHand = hand;
    }
    void FireArrow()
    {
        currentArrow.Fire (pullingValue);
        currentArrow.GetComponent<AudioSource>().Play();
        currentArrow = null;
    }    
    public void Release()
    {
        if (pullingValue > .25f)
        {
            FireArrow();
            //reset pulling arrow values
            pullingHand = null;
            pullingValue = 0;
            pullingAnimator.SetFloat("Blend", 0);
            Statistics.instance.tries--;
            Debug
                .Log("consumed arches : " +
                (Statistics.instance.intialTries - Statistics.instance.tries));
            if (Statistics.instance.tries > 0 && Player.instance.canPlay)
            {
                //Pole the next arrow
                PoleArrowRoutine = StartCoroutine(PoleArrow(currentArrowNo));
            }
            else if (Statistics.instance.tries == 0)
            {
                //Debug.Log("Statistics.instance.tries == 0");
                arrow[currentArrowNo - 1]
                    .GetComponent<LastProjectile>()
                    .ReleaseLastProjectile();
                //Debug.Log("currentArrowNo - 1 "+ (currentArrowNo - 1));
            }
            Statistics.instance.StopResponseTimeCounter();

            OnRelease(); //this event to tell other scripts that we had released abow
        }
    }

}