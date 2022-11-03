using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGrowth : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    bool isBloomed;

    int numberOfParticle;

    [SerializeField]
    int particleNeeded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim = gameObject.GetComponent<Animator>();
        isBloomed = false;
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.name);

        numberOfParticle++;
        Debug.Log(numberOfParticle);

        if (anim.GetBool("isBloom"))
        {
            Debug.Log("Flower is already Bloomed");
        }
        if (!anim.GetBool("isBloom") && numberOfParticle > particleNeeded)
        {
            anim.SetBool("isBloom", true);
        }

    }
}
