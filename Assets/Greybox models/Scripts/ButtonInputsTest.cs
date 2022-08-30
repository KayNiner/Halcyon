using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.VR;
using Liminal.SDK;
using UnityEngine;

public class ButtonInputsTest : MonoBehaviour

    TrailRenderer trail;
{
    // Start is called before the first frame update
    void Start()
    {
        TrailRenderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var primaryInput = VRDevice.Device.PrimaryInputDevice;

        if (primaryTarget)
    }
}
