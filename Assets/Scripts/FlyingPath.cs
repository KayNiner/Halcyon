using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPath : MonoBehaviour
{
    [SerializeField]
    //public Transform flyingPath;
    public GameObject objectToMoveOnPath;
    [SerializeField]
    List<GameObject> flyingPaths = new List<GameObject>();

    public float flySpeed= 0.1f;
    

    // Start is called before the first frame update
    void Start()
    {
    objectToMoveOnPath.transform.position = flyingPaths[0].transform.position;
      
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(path1());
    }

    IEnumerator path1()
    {
        Vector3 vector3 = objectToMoveOnPath.transform.position;
        for (int i = 0; i < flyingPaths.Count; i++)
        {
            transform.position = Vector3.Lerp(vector3, flyingPaths[i].transform.position, flySpeed);
            yield return new WaitForEndOfFrame();    
        }
        yield return new WaitForSeconds(1f);
    }
   

   
}
