using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDelay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Rain", 10);//this will happen after 10 seconds
    }

    public void Rain()
    {
        Debug.Log("Its Raining");
    }
  
}
