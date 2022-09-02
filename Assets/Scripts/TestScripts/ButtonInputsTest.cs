using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;

public class ButtonInputsTest : MonoBehaviour
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
        //get the active primary input device every frame
        var primaryInput = VRDevice.Device.PrimaryInputDevice;

        if (primaryInput.GetButtonDown(VRButton.Trigger) || Input.GetMouseButtonDown(0))
        {
            trail.emitting = true;
        }
        if (primaryInput.GetButtonUp(VRButton.Trigger) || Input.GetMouseButtonUp(0))
        {
            trail.emitting = false;
        }
    }
}
