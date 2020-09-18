using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemySC : MonoBehaviour
{
    [Tooltip("% chance the enemy will attack after waiting")]
    public int chanceToAttack = 15;
    [Tooltip("how long the enemy waits before moving")]
    public float waitTime = 0.8f;
    [Tooltip("how fast the enemy moves between positions")]
    public float speed = 10;
    [Tooltip("which layers the enemy should look at when determining if there is something in the way of movement")]
    public LayerMask castLayers;
    [Tooltip("particle prefab for when the enemy dies")]
    public GameObject enemyParticle;
    [Tooltip("prefab for the enemy's bullets")]
    public GameObject enemyShot;
    [Tooltip("how long the enemy will try to get to its destination before timing out")]
    public float navigationTimeout;
    [Tooltip("how close the enemy will be to its destination before stopping")]
    public float distanceReach;

    public float scorePoints = 10;
    public float damage = 10; 

    public Vector3 startPos;
    private bool isQuit;
    public NavMeshAgent agent;

    //how far away the enemy can move from its start position
    [HideInInspector]
    public float moveRange = 5;

    public GroundEnemyState currentState;

    private void Start()
    {
        //get the range from the spawner at start
        if(transform.parent)
        {
            moveRange = transform.parent.GetComponent<EnemySpawner>().range;
        }
        SetState(new GroundCoverState());
    }

    void Update()
    {
        currentState.Act(this);
        transform.LookAt(Camera.main.transform.position);
    }

    public void SetState(GroundEnemyState state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit(this);
        }
        currentState = state;
        Debug.Log("Enemy in state " + currentState, gameObject);
        if (currentState != null)
        {
            currentState.OnStateEnter(this);
        }
    }

    public float GetScorePoints() 
    {
        return scorePoints; 
    }


    public void Shoot()
    {
        GameObject go = Instantiate(enemyShot);
        go.GetComponent<BulletScript>().SetDamage(damage); 
        go.transform.position = transform.position;
        go.transform.LookAt(Camera.main.transform.position);
    }

    public void MoveToLocation(Vector3 target)
    {
        agent.SetDestination(target);
        agent.isStopped = false;
    }

    private void OnDestroy()
    {
        if (!isQuit)
        {
            GameObject go = Instantiate(enemyParticle);
            go.transform.position = transform.position;
        }
    }

    //prevents error when returning to editor
    private void OnApplicationQuit()
    {
        isQuit = true;
    }
}
