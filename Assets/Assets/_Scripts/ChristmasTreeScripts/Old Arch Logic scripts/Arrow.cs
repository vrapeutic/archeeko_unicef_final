using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    [SerializeField] float speed = 1000.0f;//the speed of the arrow
    [SerializeField] Transform tip = null;
   
    Rigidbody arrowRigidbody = null; //to apply our force

    [HideInInspector]
    public bool isStopped = true; //to track whithier arrow is flying in the air or not
    Vector3 arrowLastPosition = Vector3.zero;
    WaitForSeconds waitTime;
    public Vector3 position;
    public Vector3 force;

    private void Start()
    {
        arrowRigidbody = GetComponent<Rigidbody>();
        waitTime = new WaitForSeconds(3);

        GameManager.OnEndSuccess += DisableArrow;
        GameManager.OnEndUnsuccess += DisableArrow;
    }

    private void FixedUpdate()
    {
        if (isStopped) return;
        int layerMask = 1 << 8;
        //we want to aim to the direction that the arrow travel to the ground
        arrowRigidbody.MoveRotation(Quaternion.LookRotation(arrowRigidbody.velocity, transform.up));
        //collision check
        //instead of changing project settings and heavy the experience we wil check the collision for all journey instead
        if (Physics.Linecast(arrowLastPosition, tip.position, layerMask))
        {
            Stop();
        }

        //we will store position
        arrowLastPosition = tip.position;
    }

    void Stop()
    {
        isStopped = true;
        arrowRigidbody.isKinematic = true;
        arrowRigidbody.useGravity = false;
    }

    public void Fire(float pullingValue)
    {
        isStopped = false;
        transform.parent = null;
        arrowRigidbody.isKinematic = false;
        arrowRigidbody.useGravity = true;
        force = transform.forward * (pullingValue * speed);
        arrowRigidbody.AddForce(force);
        StartCoroutine(DisableArrowIEnum());
    }
    public IEnumerator DisableArrowIEnum()
    {
        yield return waitTime;
        DisableArrow();
    }

    private void DisableArrow()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        GameManager.OnEndSuccess -= DisableArrow;
        GameManager.OnEndUnsuccess -= DisableArrow;
    }
}