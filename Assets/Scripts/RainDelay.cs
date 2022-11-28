using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDelay : MonoBehaviour
{
    public ParticleSystem rain;
    public AudioSource rainAudio;

    // Start is called before the first frame update
    void Start()
    {
        rain = GetComponent<ParticleSystem>();
        rainAudio = GetComponent<AudioSource>();

        Invoke("Rain", 75);//this will happen after 10 seconds
    }

    public void Rain()
    {
        rain.Play();
        rainAudio.Play();

        // Debug.Log("Its Raining");
    }


}
