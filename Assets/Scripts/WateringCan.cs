using Liminal.SDK.VR.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.Platform.Experimental.App.Experiences;
using Liminal.SDK.Core;
using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR;


public class WateringCan : MonoBehaviour
{
    
    public int waterDamage = 1;
    //public float sprayRate = 1.0f;
    //public float sprayRange ;

    private WaitForSeconds sprayDuration = new WaitForSeconds(1.0f);
    public AudioSource sprayAudio;
    private float nextSpray;
    public ParticleSystem spray;
    private bool playOnAwake = false;

    [SerializeField]
    GameObject sprayCone;

    // Start is called before the first frame update
    void Start()
    {
        spray = GetComponent<ParticleSystem>();
        sprayAudio = GetComponent<AudioSource>();
        sprayCone.SetActive(false);

    }

    // Update is called once per frame
    void Update()

    {

        var rightInput = GetInput(VRInputDeviceHand.Right);
        var leftInput = GetInput(VRInputDeviceHand.Left);


        if (rightInput != null)
        {
            if (rightInput.GetButtonDown(VRButton.One))
            {
                Debug.Log("Right Trigger pressed");
                spray.Play();
                sprayAudio.Play();
            }
                
        }
        if (leftInput != null)
        {
            if (leftInput.GetButtonDown(VRButton.One))
            {
                Debug.Log("Left Trigger pressed");
                spray.Play();
                sprayAudio.Play();

            }

        }

        if( Input.GetKeyDown(KeyCode.M) )
        {
            sprayCone.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.M) )
        {
            sprayCone.SetActive(false); 
        }
    }
    private IVRInputDevice GetInput(VRInputDeviceHand hand)
    {
        var device = VRDevice.Device;
        return hand == VRInputDeviceHand.Left ? device.SecondaryInputDevice : device.PrimaryInputDevice;
    }

}
