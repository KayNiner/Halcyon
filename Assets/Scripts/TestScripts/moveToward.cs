using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToward : MonoBehaviour
{
    [SerializeField]
    List<Transform> movePos;
    int posIndex;
    [SerializeField]
    float moveSpeed;
    bool isColliding;
    // Start is called before the first frame update
    void Start()
    {
        posIndex = 0;
        transform.position = movePos[posIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
        if (posIndex < movePos.Count)
        {
            Vector3 pos1 = movePos[posIndex].position;
            Vector3 pos2 = movePos[posIndex + 1].position;
            transform.position = Vector3.MoveTowards(pos1, pos2, moveSpeed * Time.time);
            Debug.Log(posIndex);
        }
        else
            return;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (isColliding) return;

        else
        {
            isColliding = true;
            if (other.gameObject.CompareTag("Node"))
            {
                Debug.Log(isColliding.ToString());
                Debug.Log("hit");
                posIndex++;

                other.gameObject.GetComponent<Collider>().enabled = false;
            }
        }

    }
   
}
