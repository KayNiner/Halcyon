using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLerp : MonoBehaviour
{
    [SerializeField]
    List<Transform> destination;

    public float flyTime = 0.01f;
    Vector3 pos1, pos2;
    public int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos1 = destination[i].position;
        

        transform.position = Vector3.Lerp(transform.position, pos1, flyTime);
    }

    void OntriggerEnter()
    {
        if (i == 0)
        { i = 1; }

        if (i == 1)
        { i = 0; }
    }
}
