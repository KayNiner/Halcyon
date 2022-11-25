using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGrowth : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    bool isBloomed;

    [SerializeField]
    List<GameObject> butterflies;
    int butterflyIndex;

    int numberOfParticle;
    [SerializeField]
    float timePassed;
    

    [SerializeField]
    int particleNeeded;

    [SerializeField]
    int hitPoint;
    // Start is called before the first frame update
    void Start()
    {
        butterflies[0].SetActive(false);
        butterflies[1].SetActive(false);
        isBloomed = false;
        anim = gameObject.GetComponent<Animator>();
        hitPoint = 500;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isBloomed)
        {
            timePassed += Time.deltaTime;

            if(timePassed > 80)
            {
                isBloomed =true;
                anim.SetBool("isBloomed", true);
                Invoke("ButterflySpawn", 1f);
            }
        }
        
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.name);

        numberOfParticle++;
        Debug.Log(numberOfParticle);

        if (anim.GetBool("isBloomed"))
        {
            Debug.Log("Flower is already Bloomed");
        }
        if (!anim.GetBool("isBloomed") && numberOfParticle > particleNeeded)
        {
            isBloomed=true;
            anim.SetBool("isBloomed", true);
            
            Invoke("ButterflySpawn", 1f);
        }
        

    }

    void OnTriggerEnter()
    {   
        
        if (anim.GetBool("isBloomed"))
        {
            Debug.Log("Flower is already Bloomed");
        }
        if (!anim.GetBool("isBloomed") && hitPoint > 0)
        {
            hitPoint -= 15;
            //isBloomed = true;
            //anim.SetBool("isBloomed", true);

           // Invoke("ButterflySpawn", 1f);
        }
    }

    void OnTriggerStay()
    {
        if (anim.GetBool("isBloomed"))
        {
            Debug.Log("Flower is already Bloomed");
        }
        if (!anim.GetBool("isBloomed") && hitPoint > 0)
        {
            hitPoint--;
            
        }
        if (!anim.GetBool("isBloomed") && hitPoint <= 0)
        {
            isBloomed = true;
            anim.SetBool("isBloomed", true);
            Invoke("ButterflySpawn", 1f);
        }
    }
        void ButterflySpawn()
    {
        butterflyIndex = Random.Range(0, 1);
        butterflies[butterflyIndex].SetActive(true); 
    }
}
