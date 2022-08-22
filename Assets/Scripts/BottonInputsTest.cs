using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;

public class BottonInputsTest : MonoBehaviour
{
    TrailRenderer trail;
    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //get active primary inputdevice every frame

        var primaryInput = VRDevice.Device.PrimaryInputDevice;

        if (primaryInput.GetButtonDown(VRButton.Trigger))
        {
            trail.emitting = true;
        }
        if (primaryInput.GetButtonUp(VRButton.Trigger))
        {
            trail.emitting = false;
        }
    }
}
