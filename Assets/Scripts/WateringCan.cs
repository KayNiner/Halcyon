﻿using Liminal.SDK.VR.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.Platform.Experimental.App.Experiences;
using Liminal.SDK.Core;
using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR;


public class WateringCan : MonoBehaviour
{
    
    public int waterDamage = 30;
    public float sprayRate = 1.0f;
    public float sprayRange ;

    private WaitForSeconds sprayDuration = new WaitForSeconds(1.0f);
    private AudioSource sprayAudio;
    private float nextSpray;
    public ParticleSystem spray;
    public bool playOnAwake = false;

    // Start is called before the first frame update
    void Start()
    {
        spray = GetComponent<ParticleSystem>();
        sprayAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()

    {

        var avatar = VRAvatar.Active;
        if (avatar == null)
            return;

        var rightInput = GetInput(VRInputDeviceHand.Right);
        var leftInput = GetInput(VRInputDeviceHand.Left);


        if (rightInput != null)
        {
            if (rightInput.GetButtonDown(VRButton.One))
            {
                spray.Play();
            }
                
        }
       
    }
    private IVRInputDevice GetInput(VRInputDeviceHand hand)
    {
        var device = VRDevice.Device;
        return hand == VRInputDeviceHand.Left ? device.SecondaryInputDevice : device.PrimaryInputDevice;
    }
}
