using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyAnimationOffset : MonoBehaviour
{
    public Animator anim;
    public float offset;

    void Start()
    {
         gameObject.GetComponent<Animator>().SetFloat("Offset", Random.Range(0f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
         
       
    }
}
