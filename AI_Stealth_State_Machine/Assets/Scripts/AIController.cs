using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AIController : StateMachine {
    private State currentState;

    //instances of each state
    public readonly Patrol patrolState = new Patrol();
    public readonly Chase chaseState = new Chase();
    public readonly GetToPlayerLastPosition getToPlayerLastPosition = new GetToPlayerLastPosition();
    public readonly Seek seekState = new Seek();

    public GameObject aiObject;
    public Vector3 playerLastPosition;
    private NavMeshAgent navAgent;

    //list of all points in patrol route
    public List<Vector2> patrolPositions = new List<Vector2>{ new Vector2(47, 41), new Vector2(35, 41), new Vector2(23, 41), new Vector2(11, 41),
                                         new Vector2(11, 29), new Vector2(23, 29), new Vector2(35, 29), new Vector2(47, 29),
                                         new Vector2(47, 17), new Vector2(35, 17), new Vector2(23, 17), new Vector2(11, 17),
                                         new Vector2(11, 5) , new Vector2(23, 5) , new Vector2(35, 5) , new Vector2(47, 5) ,
    };

    //starting point of patrol
    [SerializeField]
    int patrolIndex = 0;    

    [SerializeField]
    public Transform goal;

    [SerializeField]
    public GameObject playerObject;


    private MeshRenderer renderer;

    [SerializeField]
    Material[] materials;
    public void SetState(State a_state, int materialIndex) {
        if (currentState != null) {
            currentState.Exit(this);
        }
        
        currentState = a_state;
        renderer.material = materials[materialIndex];

        currentState.Enter(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        goal.position = new Vector3(patrolPositions[patrolIndex].x, gameObject.transform.position.y, patrolPositions[patrolIndex].y);
        gameObject.transform.position = goal.position;
        renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.material = materials[0];
        aiObject = gameObject;
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        navAgent.destination = goal.position;

        currentState = patrolState;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Execute(this);//update function for current state
    }

    public void NewRandomGoal() {
        //increase patrol index to next patrol point
        if (patrolIndex == patrolPositions.Count-1) {
            patrolIndex = 0;
        } else {
            patrolIndex++;
        }

             
        //sets goal position to patrol position
        Vector3 newGoal = goal.transform.position;
        newGoal.x= patrolPositions[patrolIndex].x;
        newGoal.z = patrolPositions[patrolIndex].y;
        goal.transform.position = newGoal;
        navAgent.destination = goal.position;
    }

    public void ChasePlayer(Vector3 a_position) {
        //updates goal position based on player location
        goal.position = a_position;

        navAgent.destination = goal.position;
    }



    public void ResumePatrol(Vector2 goalPosition) {
        //resets the patrol index to the closest point after AI has finished searching for the player
        patrolIndex=patrolPositions.IndexOf(goalPosition);
        goal.position = new Vector3(patrolPositions[patrolIndex].x, aiObject.transform.position.y, patrolPositions[patrolIndex].y);
        navAgent.destination = goal.position;
    }

    public void SeekGoal() {
        //once player is close enough to the random goal the position is changed to change direction
        if(Vector3.Distance(goal.position, aiObject.transform.position) < 1.5f) {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * 15;

            randomDirection += aiObject.transform.position;

            NavMeshHit navHit;

            NavMesh.SamplePosition(randomDirection, out navHit, 10, 1);

            while (!navHit.hit) {
                randomDirection = UnityEngine.Random.insideUnitSphere * 15;

                randomDirection += aiObject.transform.position;
                NavMesh.SamplePosition(randomDirection, out navHit, 10, 1);
            }
            goal.position = navHit.position;
            goal.position =  new Vector3(goal.position.x, aiObject.transform.position.y, goal.position.z);

            navAgent.destination = goal.position;
        }
        
    }
}
