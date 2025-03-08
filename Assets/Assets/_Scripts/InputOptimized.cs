using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using PaintIn3D;
public class InputOptimized : MonoBehaviour
{
    [SerializeField]
    BowTest bow = null;
    [SerializeField]
    GunInput gun;

    public GameObject oppositeController = null;//controller that not held the Bow
                                                // public OVRInput.Controller controller = OVRInput.Controller.None;
    public InputActionReference actionReference;

    private void Awake()
    {
        actionReference.action.started += Pull;
        actionReference.action.canceled += Realease;
        // RenderSettings.fog = true;
    }

    private void Start()
    {
        bow = FindObjectOfType<BowTest>();
        gun = FindObjectOfType<GunInput>();
    }
    private void OnDestroy()
    {
        actionReference.action.started -= Pull;
        actionReference.action.canceled -= Realease;
    }


    private void OnRelease(XRBaseInteractor interactor)
    {
        bow.Release();
    }
    private void OnPull(XRBaseInteractor interactor)
    {
        if (Statistics.instance.isArchery) bow.Pull(oppositeController.transform);
        else gun.ShootNewBullet();
    }


    private void Pull(InputAction.CallbackContext context)
    {
        Debug.Log("pull");
        if (Statistics.instance.isArchery) bow.Pull(oppositeController.transform);
        else
        {
            Debug.Log("gun shoot");

            gun.ShootNewBullet();
        }
    }

    private void Realease(InputAction.CallbackContext context)
    {
        if (Statistics.instance.isArchery) bow.Release();
       // else gun.ShootNewBullet();
    }
}
