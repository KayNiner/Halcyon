using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationStateMachine : MonoBehaviour
{
    //Create state for butterfly AI.
    public enum STATES { IDLE, FLYING, LEAVING}
    public STATES currentState;

    private NavMeshAgent agent;
    public Transform target;

    public float sightRadius;

    //target location nodes
    [SerializeField] Transform[] nodes;
    int currentNode = 0;

    //Awake
    private void Awake()
    {
        currentState = STATES.IDLE;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(butterflyFSM());
    }

    #region State Machine Coroutines

    IEnumerator butterflyFSM()  //FSM  = finite state machine
    {
        while (true)
        {
            yield return StartCoroutine(currentState.ToString());
        }
    }
    IEnumerator IDLE()
    {
        //Enter the state: run behaviour on state start here
        Debug.Log("IDLE");
        int idleTime = 0;

        //Execute State: run the main behaviour
        while(currentState == STATES.IDLE)
        {
            yield return new WaitForSeconds(1);
            idleTime += 1;

        }
        if (idleTime >= 1) 
        {
            currentState = STATES.FLYING;
        }

        //Exit states
        Debug.Log("Start Flying");

    }

    IEnumerator FLYING()
    {
        //Enter the state: run behaviour on state start here
        Debug.Log("FLYING");
        

        //Execute State: run the main behaviour
        while (currentState == STATES.FLYING)
        {
            agent.SetDestination(nodes[currentNode].position);
            if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance )
            {
                currentNode = (currentNode + 1) % nodes.Length;
            }
            yield return new WaitForEndOfFrame();
        }

    }

    #endregion
}
