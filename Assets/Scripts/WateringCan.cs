using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public int waterDamage = 30;
    public float sprayRate = 1.0f;
    public float sprayRange ;

    private WaitForSeconds sprayDuration = new WaitForSeconds(1.0f);
    private AudioSource sprayAudio;
    private float nextSpray;
    public ParticleSystem spray;
    
    // Start is called before the first frame update
    void Start()
    {
        spray = GetComponent<ParticleSystem>();
        sprayAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            spray.Play();
            
        }
    }
}
