using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFlowerHealth : MonoBehaviour
{
    [Tooltip("Health Amounts")]
    public int maxHealth, startingHealth, currentHealth;
    public bool canBeDamaged;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
