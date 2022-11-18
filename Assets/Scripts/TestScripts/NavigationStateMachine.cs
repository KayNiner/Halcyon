using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationStateMachine : MonoBehaviour
{
    //Create state for butterfly AI.
    public enum STATES { SET1, SET2, SET3, SET4, FINAL, FLYING}
    public STATES currentState;

    private NavMeshAgent agent;
    public Transform target;

    public float sightRadius;
    public Animator anim;
    bool isFlying;

    //target location nodes
    [SerializeField] Transform[] idleNodes;

    //[SerializeField] Transform[] flyingNodes;
    [SerializeField] int currentNode = 0;

    //node Holder
    [SerializeField] GameObject nodeHolder;

    //List of nodes
    [SerializeField] List<Transform> flyingNodes = new List<Transform>();
   

    //Awake
    private void Awake()
    {
        //currentState = STATES.FLYING;
    }

    private void Start()
    {
        //Grabbing navmesh agent and animator 
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //StartCoroutine(butterflyFSM());

        //Assigning Waypoint/Adding them to the list
        nodeHolder = GameObject.Find("Waypoints");
        

        foreach (Transform t in nodeHolder.transform)
        {
            flyingNodes.Add(t);
        }
        

    }
    void OnEnable()
    {
        anim = GetComponent<Animator>();
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
    /*IEnumerator IDLE()
    {
        //Enter the state: run behaviour on state start here
        Debug.Log("IDLE");
        isFlying = false;
        int idleTime = 0;
        

        //Execute State: run the main behaviour
        while(currentState == STATES.IDLE)
        {
            agent.SetDestination(idleNodes[0].position);
        

            yield return new WaitForEndOfFrame();
            anim.SetBool("isFlying", false);
            yield return new WaitForSeconds(20);
            idleTime += 1;
            
            currentState = STATES.FLYING;
        }
        
        //Exit states
        Debug.Log("Start Flying")
    }*/


    IEnumerator FLYING()
    {
        //Enter the state: run behaviour on state start here
        Debug.Log("FLYING");
        anim.SetBool("isFlying", true);
        

        //Execute State: run the main behaviour
        while (currentState == STATES.FLYING)
        {
            agent.SetDestination(flyingNodes[currentNode].position);
            if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance )
            {
                currentNode = Random.Range(0, flyingNodes.Count);
                //currentNode = (currentNode + 1) % flyingNodes.Length;
            }
            yield return new WaitForEndOfFrame();
        }

    }
    
    IEnumerable SET1()
    {
        //State Entries
        Debug.Log("Butterflies are flying on set1 path");
        anim.SetBool("isFlying", true);

        //Looping the behaviours;
        while(currentState == STATES.SET1)
        {
            agent.SetDestination(flyingNodes[currentNode].position);
            if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                currentNode++;
                if(currentNode >=3)
                {
                    currentNode = 0;
                }
            }
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    IEnumerable SET2()
    {
        //State Entries
        Debug.Log("Butterflies are flying on set1 path");
        anim.SetBool("isFlying", true);
        currentNode = 4;
        //Looping the behaviours;
        while (currentState == STATES.SET1)
        {
            agent.SetDestination(flyingNodes[currentNode].position);
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
              
                currentNode++;
                if (currentNode >= 7)
                {
                    currentNode = 4;
                }
            }
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    IEnumerable SET3()
    {
        //State Entries
        Debug.Log("Butterflies are flying on set1 path");
        anim.SetBool("isFlying", true);
        currentNode = 8;
        //Looping the behaviours;
        while (currentState == STATES.SET1)
        {
            agent.SetDestination(flyingNodes[currentNode].position);
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {

                currentNode++;
                if (currentNode >= 10)
                {
                    currentNode = 8;
                }
            }
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
    IEnumerable SET3()
    {
        //State Entries
        Debug.Log("Butterflies are flying on set1 path");
        anim.SetBool("isFlying", true);
        currentNode = 11;
        //Looping the behaviours;
        while (currentState == STATES.SET1)
        {
            agent.SetDestination(flyingNodes[currentNode].position);
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {

                currentNode++;
                if (currentNode >= 14)
                {
                    currentNode = 11;
                }
            }
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    #endregion
}
