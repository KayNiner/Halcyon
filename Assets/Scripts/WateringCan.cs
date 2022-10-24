using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public int waterDamage = 1;
    public float sprayRate = 1.0f;
    public float sprayRange ;

    private WaitForSeconds sprayDuration = new WaitForSeconds(1.0f);
    private AudioSource sprayAudio;
    private float nextSpray;
    private bool isfiring;

    public GameObject particleEffect;

    // Start is called before the first frame update
    void Start()
    {
        particleEffect = GetComponent<GameObject>();
        sprayAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire"))
        {
            
            isfiring = true;

        }
    }

    
}
