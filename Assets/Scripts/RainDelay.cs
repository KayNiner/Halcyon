using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDelay : MonoBehaviour
{
    public ParticleSystem rain;

    // Start is called before the first frame update
    void Start()
    {
        rain = GetComponent<ParticleSystem>();

        Invoke("Rain", 10);//this will happen after 10 seconds
    }

    public void Rain()
    {
        rain.Play();
        Debug.Log("Its Raining");
    }

    
}
