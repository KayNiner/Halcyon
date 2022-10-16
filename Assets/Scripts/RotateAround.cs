using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public GameObject target;
    public float orbit;
    public float spin;

    public void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.up, orbit * Time.deltaTime);
        Spin();
    }
    public void Spin()
    {
        transform.Rotate(Vector3.up, spin * Time.deltaTime);
    }
}
