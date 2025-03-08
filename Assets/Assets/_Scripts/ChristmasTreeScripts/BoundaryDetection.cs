using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BoundaryDetection : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    Transform camera;
    AudioSource audioSource;
    WaitForSeconds aPartSecond;
    Vector3 newPosition;
    bool notFocused;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        //Debug.Log("Start");
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        aPartSecond = new WaitForSeconds(.2f);
        newPosition = new Vector3();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = audioClip;
        audioSource.playOnAwake = false;
        CheckDetector();
#endif
    }

    async void CheckDetector()
    {/*/
        //Debug.Log("CheckDetector");
        while (true)
        {
            await aPartSecond;
            if (OVRManager.boundary.GetVisible() && notFocused)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                    SetAudioSourcePosition();
                    OVRInput.SetControllerVibration(1, 0.7f, OVRInput.Controller.RTouch);
                    OVRInput.SetControllerVibration(1, 0.7f, OVRInput.Controller.LTouch);
                }
            }
            else
            {
//                Debug.Log("else " + "OVRManager.boundary.GetVisible() && notFocused");
                audioSource.Stop();
                OVRInput.SetControllerVibration(1, 0, OVRInput.Controller.RTouch);
                OVRInput.SetControllerVibration(1, 0, OVRInput.Controller.LTouch);
            }
        }*/
    }
    private void SetAudioSourcePosition()
    {
        newPosition.x = camera.position.x;
        newPosition.y = camera.position.y;
        newPosition.z = camera.position.z + 1;
        transform.position = newPosition;
    }
    async void HMDMountedFunc()
    {
        //Debug.Log("focus: ");
        await avoidOnFocusSound();
    }
    async Task avoidOnFocusSound()
    {
        notFocused = false;
        await new WaitForSeconds(5);
        notFocused = true;
    }

    private void OnEnable()
    {
      //  OVRManager.HMDMounted += HMDMountedFunc;
    }

    private void OnDisable()
    {
       // OVRManager.HMDMounted -= HMDMountedFunc;
    }
}
