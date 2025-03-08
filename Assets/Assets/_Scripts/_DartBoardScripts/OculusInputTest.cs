using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
//using OVR;

public class OculusInputTest : MonoBehaviour
{

    public BowTest bow = null;
    public GameObject oppositeController = null;//controller that not held the Bow
                                                // public OVRInput.Controller controller = OVRInput.Controller.None;
    public XRController controller;

    private void Update()
    {
        //1
     //   if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
    // if(controller.inputDevice.)
            bow.Pull(oppositeController.transform);

        //5(when i have a pulling value)
       // if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, controller))
            bow.Release();
    }

}
