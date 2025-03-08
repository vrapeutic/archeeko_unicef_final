using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    Rigidbody ballPhysics;
    [SerializeField] int force;

    public delegate void AttackAction();
    public static event AttackAction OnAttack;

    private void Start()
    {
         ballPhysics = GetComponent<Rigidbody>();
    }
    public  void MoveForwardPlayer()
    {
        StartCoroutine(MoveForwardIenum());
    }

    IEnumerator MoveForwardIenum()
    {
        this.gameObject.transform.SetParent(null);
        yield return new WaitForSeconds(1f);
        Debug.Log("Attack");
        gameObject.GetComponent<AudioSource>().Play();
        //calculate diraction of the ball
        Vector3 dir = target.position - transform.position;
        transform.rotation = Quaternion.LookRotation(dir);
        //add force to the ball
        ballPhysics.AddForce(dir * force);
        Statistics.instance.RocketresponeTimeBool = true;
        OnAttack();
        //Destroy the ball after two seconds
        Destroy(this.gameObject, 2f);
    }  
}
