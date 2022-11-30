using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationStateMachine : MonoBehaviour
{
    //Create state for butterfly AI.
    public enum STATES { SET1, SET2, SET3, SET4, FINAL, SET1Bot,SET2Bot,SET3Bot,SET4Bot,FINALBot}
    public STATES currentState;

    private NavMeshAgent agent;
    public Animator anim;
    bool isFlying;

    public enum finalDestination {TOP, BOTTOM}
    public GameObject planet;

    //target location nodes

    //[SerializeField] Transform[] flyingNodes;
    [SerializeField] int currentNode = 0;

    //node Holder
    [SerializeField] public GameObject nodeHolder1, nodeHolder2, nodeHolder3, nodeHolder4, nodeHolderFinal;
    [SerializeField] public GameObject nodeHolder1Bot, nodeHolder2Bot, nodeHolder3Bot, nodeHolder4Bot, nodeHolderFinalBot;

    public finalDestination finalWaypoint;
    //List of nodes
    /*[SerializeField]*/List<Transform> flyingNodes1 = new List<Transform>();
    /*[SerializeField]*/List<Transform> flyingNodes2 = new List<Transform>();
    /*[SerializeField]*/List<Transform> flyingNodes3 = new List<Transform>();
    /*[SerializeField]*/List<Transform> flyingNodes4= new List<Transform>();
    /*[SerializeField]*/List<Transform> flyingNodesFinal = new List<Transform>();
    //List of bot node
    /*[SerializeField]*/List<Transform> flyingNodes1Bot = new List<Transform>();
    /*[SerializeField]*/List<Transform> flyingNodes2Bot = new List<Transform>();
    /*[SerializeField]*/List<Transform> flyingNodes3Bot = new List<Transform>();
    /*[SerializeField]*/List<Transform> flyingNodes4Bot = new List<Transform>();
    /*[SerializeField]*/List<Transform> flyingNodesFinalBot = new List<Transform>();

    //Reference ScreeFade Script
    public int bloomedFlower;
    float circlingTimer;
    float lerpSpeed = 0.001f;
    
    public ExitScreenFade exitScreen;




    [Header("Scale Script by Darcy")]
    public float maxSize;
    public float growFactor;
    public float waitTime;

    //Awake
    private void Awake()
    {
        bloomedFlower = exitScreen.numberOfBloomedFlower;
        anim = GetComponent<Animator>();
        //currentState = STATES.FLYING;
        /*nodeHolder1 = GameObject.Find("Set1");
        nodeHolder2 = GameObject.Find("Set2");
        nodeHolder3 = GameObject.Find("Set3");
        nodeHolder4 = GameObject.Find("Set4");
        nodeHolderFinal = GameObject.Find("Final");
        //bot node
        nodeHolder1Bot = GameObject.Find("Set1_bot");
        nodeHolder2Bot = GameObject.Find("Set2_bot");
        nodeHolder3Bot = GameObject.Find("Set3_bot");
        nodeHolder4Bot = GameObject.Find("Set4_bot");
        nodeHolderFinalBot = GameObject.Find("Final_bot");*/

        agent = GetComponent<NavMeshAgent>();
        //Flying node top
        foreach (Transform t in nodeHolder1.transform)
        {
            flyingNodes1.Add(t);
        }
        foreach (Transform t in nodeHolder2.transform)
        {
            flyingNodes2.Add(t);
        }
        foreach (Transform t in nodeHolder3.transform)
        {
            flyingNodes3.Add(t);
        }
        foreach (Transform t in nodeHolder4.transform)
        {
            flyingNodes4.Add(t);
        }
        foreach (Transform t in nodeHolderFinal.transform)
        {
            flyingNodesFinal.Add(t);
        }

        //Flying node bot
        foreach (Transform t in nodeHolder1Bot.transform)
        {
            flyingNodes1Bot.Add(t);
        }
        foreach (Transform t in nodeHolder2Bot.transform)
        {
            flyingNodes2Bot.Add(t);
        }
        foreach (Transform t in nodeHolder3Bot.transform)
        {
            flyingNodes3Bot.Add(t);
        }
        foreach (Transform t in nodeHolder4Bot.transform)
        {
            flyingNodes4Bot.Add(t);
        }
        foreach (Transform t in nodeHolderFinalBot.transform)
        {
            flyingNodesFinalBot.Add(t);
        }


    }

    private void Start()
    {
        //StartCoroutine(butterflyFSM());
        //Grabbing navmesh agent and animator 
        

        //Assigning Waypoint/Adding them to the list



    }
    void OnEnable()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Scale());
        StartCoroutine(butterflyFSM());
        
    }
    
    private void Update()
    {
    
        bloomedFlower = exitScreen.numberOfBloomedFlower;
      
        if ((bloomedFlower >=10) && finalWaypoint == finalDestination.TOP)
        {
            currentState = STATES.FINAL;
            circlingTimer += Time.deltaTime;
        }
        else if ((bloomedFlower >=10) && (finalWaypoint == finalDestination.BOTTOM))
        {
            currentState=STATES.FINALBot;
            circlingTimer += Time.deltaTime;
        }

        if (circlingTimer >= 17)
        {
            StopAllCoroutines();
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            transform.position = Vector3.Lerp(transform.position, planet.transform.position, lerpSpeed);
        }
    }
    //Scale Enum
    IEnumerator Scale()
    {
        float timer = 0;
        Debug.Log("Start Scaling");
        while (true) //while the butterfly is growing in size after the flower has bloomed
        {
            while (maxSize > transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
                yield return null;
            }
            // reset the timer

            yield return new WaitForSeconds(waitTime);

            //timer = 0;
            //while (1 < transform.localScale.x)
            //{
            //    timer += Time.deltaTime;
            //    transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
            //    yield return null;
            //}

            //timer = 0;
            //yield return new WaitForSeconds(waitTime);
        }

    }

    //State MACHINE

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


    /*IEnumerator FLYING()
    {
        //Enter the state: run behaviour on state start here
        Debug.Log("FLYING");
        anim.SetBool("isFlying", true);
        

        //Execute State: run the main behaviour
        while (currentState == STATES.FLYING)
        {
            agent.SetDestination(flyingNodes1[currentNode].position);
            if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance )
            {
                currentNode = Random.Range(0, flyingNodes1.Count);
                //currentNode = (currentNode + 1) % flyingNodes.Length;
            }
            yield return new WaitForEndOfFrame();
        }

    }
    */

    //Enum Top Nodes
    IEnumerator SET1()
    {
        //State Entries
        Debug.Log("Butterflies are flying on set1 path");
        anim.SetBool("isFlying", true);
        //Looping the behaviours;
        while (currentState == STATES.SET1)
        {
            agent.SetDestination(flyingNodes1[currentNode].position);
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {

                currentNode = (currentNode + 1) % flyingNodes1.Count;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    IEnumerator SET2()
    {
        //State Entries
        Debug.Log("Butterflies are flying on set1 path");
        anim.SetBool("isFlying", true);
        //Looping the behaviours;
        while (currentState == STATES.SET2)
        {
            agent.SetDestination(flyingNodes2[currentNode].position);
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {

                currentNode = (currentNode + 1) % flyingNodes2.Count;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    IEnumerator SET3()
    {
        //State Entries
        Debug.Log("Butterflies are flying on set1 path");
        anim.SetBool("isFlying", true);
        //Looping the behaviours;
        while (currentState == STATES.SET3)
        {
            agent.SetDestination(flyingNodes3[currentNode].position);
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {

                currentNode = (currentNode + 1) % flyingNodes3.Count;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
    IEnumerator SET4()
    {
        //State Entries
        Debug.Log("Butterflies are flying on set1 path");
        anim.SetBool("isFlying", true);
        //Looping the behaviours;
        while (currentState == STATES.SET4)
        {
            agent.SetDestination(flyingNodes4[currentNode].position);
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {

                currentNode = (currentNode + 1) % flyingNodes4.Count;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    IEnumerator FINAL()
    {
        //State Entries
        Debug.Log("Butterflies are flying on set1 path");
        anim.SetBool("isFlying", true);
        //Looping the behaviours;
        while (currentState == STATES.FINAL)
        {
            agent.SetDestination(flyingNodesFinal[currentNode].position);
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {

                currentNode = (currentNode + 1) % flyingNodesFinal.Count;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
    /*IEnumerator RANDOM()
    {
        //State Entries
        Debug.Log("Butterflies are flying on set1 path");
        anim.SetBool("isFlying", true);

        //Looping the behaviours;
        while (currentState == STATES.RANDOM)
        {
            agent.SetDestination(flyingNodes4[currentNode].position);
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                currentNode = Random.Range(0, flyingNodes4.Count);
                if (currentNode >= 3)
                {
                    currentNode = 0;
                }
            }
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
    */

    //Enum Bot Nodes
    IEnumerator SET1Bot()
    {
        //State Entries
        Debug.Log("Butterflies are flying on set1 bot path");
        anim.SetBool("isFlying", true);
        //Looping the behaviours;
        while (currentState == STATES.SET1Bot)
        {
            agent.SetDestination(flyingNodes1Bot[currentNode].position);
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {

                currentNode = (currentNode + 1) % flyingNodes1Bot.Count;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
    IEnumerator SET2Bot()
    {
        //State Entries
        Debug.Log("Butterflies are flying on set2 bot path");
        anim.SetBool("isFlying", true);
        //Looping the behaviours;
        while (currentState == STATES.SET2Bot)
        {
            agent.SetDestination(flyingNodes2Bot[currentNode].position);
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {

                currentNode = (currentNode + 1) % flyingNodes2Bot.Count;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
    IEnumerator SET3Bot()
    {
        //State Entries
        Debug.Log("Butterflies are flying on set3 bot path");
        anim.SetBool("isFlying", true);
        //Looping the behaviours;
        while (currentState == STATES.SET3Bot)
        {
            agent.SetDestination(flyingNodes3Bot[currentNode].position);
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {

                currentNode = (currentNode + 1) % flyingNodes3Bot.Count;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
    IEnumerator SET4Bot()
    {
        //State Entries
        Debug.Log("Butterflies are flying on set 4 path");
        anim.SetBool("isFlying", true);
        //Looping the behaviours;
        while (currentState == STATES.SET4Bot)
        {
            agent.SetDestination(flyingNodes4Bot[currentNode].position);
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {

                currentNode = (currentNode + 1) % flyingNodes4Bot.Count;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
    IEnumerator FINALBot()
    {
        //State Entries
        Debug.Log("Butterflies are flying on set1 path");
        anim.SetBool("isFlying", true);
        //Looping the behaviours;
        while (currentState == STATES.FINALBot)
        {
            agent.SetDestination(flyingNodesFinalBot[currentNode].position);
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {

                currentNode = (currentNode + 1) % flyingNodesFinalBot.Count;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
    #endregion
}
