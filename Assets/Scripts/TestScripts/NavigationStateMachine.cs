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
    public Animator anim;
    bool isFlying;

    //target location nodes
    [SerializeField] Transform[] idleNodes;

    //[SerializeField] Transform[] flyingNodes;
    [SerializeField] int currentNode = 0;

    [SerializeField] GameObject nodeHolder;
    [SerializeField] List<Transform> flyingNodes = new List<Transform>();
    


    //Awake
    private void Awake()
    {
        currentState = STATES.FLYING;
        
        

    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //StartCoroutine(butterflyFSM());
        nodeHolder = GameObject.Find("WavyGround");
        foreach (Transform t in nodeHolder.transform)
        {
            flyingNodes.Add(t);
        }
        currentNode = Random.Range(0, flyingNodes.Count);

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
    IEnumerator IDLE()
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
        Debug.Log("Start Flying");

    }

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

        yield return null;
    }

    #endregion
}
