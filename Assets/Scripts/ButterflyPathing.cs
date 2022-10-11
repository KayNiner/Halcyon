using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyPathing : MonoBehaviour
{
    [SerializeField]
    List<Transform> destination;
    public float flyTime = 0.7f;
    //Vector3 pos1, pos2;

    // Start is called before the first frame update
    void Start()
    {
        //Vector3 pos1 = destination[0].position;
        //Vector3 pos2 = destination[1].position;
    }

    // Update is called once per frame
    void Update()
    {
     
        //transform.position = Vector3.Lerp(pos1, pos2, flyTime); 

    }

    /*IEnumerator part1()
    {
        Vector3 pos1 = destination[1].position;
        //Vector3 pos2 = destination[1].position;

        transform.position = Vector3.Lerp(transform.position, pos1, flyTime);


        yield return new WaitForSeconds(1);

    }
    IEnumerator part2()
    {
        StartCoroutine(part1());
        Vector3 pos2 = destination[2].position;
        transform.position = Vector3.Lerp(transform.position, pos2, flyTime);

        yield return new WaitForSeconds(1);
    }*/
}
