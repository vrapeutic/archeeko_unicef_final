using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
public class BowInput : MonoBehaviour
{
    public BowTest bow = null;
    public GameObject oppositeController = null;//controller that not held the Bow
                                               // public OVRInput.Controller controller = OVRInput.Controller.None;
    public InputActionReference actionReference;

    private void OnEnable()
    {
        actionReference.action.started += Pull;
        actionReference.action.canceled += Realease;
    }

    
    private void OnDisable()
    {
        actionReference.action.started -=  Pull;
        actionReference.action.canceled -= Realease;
    }


    private void OnRelease(XRBaseInteractor interactor)
    {
        bow.Release();
    }
    private void OnPull(XRBaseInteractor interactor)
    {
            bow.Pull(oppositeController.transform);
    }


    private void Pull(InputAction.CallbackContext context)
    {
            bow.Pull(oppositeController.transform);
    }

    private void Realease(InputAction.CallbackContext context)
    {
            bow.Release();
    }

}
