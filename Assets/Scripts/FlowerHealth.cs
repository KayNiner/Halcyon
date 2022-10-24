using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float health;
    public bool beingWatered = false;
    public bool isBloom;
    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
    }
    public void waterPlant(float waterAmount)
    {
        if (!beingWatered)
        {
            Debug.Log("Being watered");
            health -= waterAmount;
            beingWatered = true;
        }
    }

    public virtual void Bloom()
    {
        Debug.Log("Bloomed");
        isBloom = true;
    }
   
}
