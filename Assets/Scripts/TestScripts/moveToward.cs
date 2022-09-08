using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToward : MonoBehaviour
{
    [SerializeField]
    List<Transform> movePos;
    int posIndenx;
    [SerializeField]
    float moveSpeed;


    void Awaken()
    {
        

    }   
    // Start is called before the first frame update
    void Start()
    {
        posIndenx = 0;
        transform.position = movePos[posIndenx].position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos1 = movePos[posIndenx].position;
        Vector3 pos2 = movePos[posIndenx+1].position;
        transform.position = Vector3.MoveTowards(pos1, pos2, moveSpeed*Time.time);
        Debug.Log(posIndenx);

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Node")
        {
            Debug.Log("hit");
            posIndenx= posIndenx +1;
            
            other.gameObject.GetComponent<Collider>().enabled = false;  
        }
        

    }
   
}
