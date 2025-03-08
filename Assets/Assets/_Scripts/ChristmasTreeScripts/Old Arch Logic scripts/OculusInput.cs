using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using OVR;

public class OculusInput : MonoBehaviour
{

    public Bow bow = null;
    public GameObject oppositeController = null;//controller that not held the Bow
  //  public OVRInput.Controller controller = OVRInput.Controller.None;

    private void Update()
    {
       // if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
            bow.Pull(oppositeController.transform);

      //  if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, controller))
            bow.Release();
    }

}
