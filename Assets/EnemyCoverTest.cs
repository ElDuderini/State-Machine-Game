using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCoverTest : MonoBehaviour
{
    public float colliderRange;
    public float coverRange;
    public float waitTime;
    public float speed;
    public string state = "wait";
    private float time;
    private Vector3 startPos;
    private Vector3 newPos;

    private void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        //these states are temporary and will be changed to real states later
        if (state == "wait")
        {
            time += Time.deltaTime;

            if (time >= waitTime)
            {
                newPos = FindNewPosition();
            }
        }

        if (state == "move")
        {
            transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
            if (transform.position == newPos)
            {
                newPos = Vector3.zero;
                state = "wait";
            }
        }
    }

    private Vector3 FindNewPosition()
    {
        time = 0;


        for (int i = 0; i < 100; i++)
        {
            //choose a random point in range
            Vector3 pos = (Random.insideUnitSphere * coverRange) + startPos;
            //first, find if there is an object between the enemy and player to determine if it is under cover (cover and uncovered will probably be two different states later)
            //then check if the new position would be the opposite (e.g. if it wasn't behind cover, see if the new position would be)
            //next, make sure the enemy wouldn't be partially inside an object (might change this to use the collider instead of number)
            //Then, make sure there is nothing between the enemy's current position and the new position
            if (!Physics.Linecast(transform.position, Camera.main.transform.position) && Physics.Linecast(pos, Camera.main.transform.position) && !Physics.CheckSphere(pos, colliderRange) && !Physics.Linecast(transform.position, pos))
            {
                print("Finding Cover");
                state = "move";
                return pos;
            }
            else if (Physics.Linecast(transform.position, Camera.main.transform.position) && !Physics.Linecast(pos, Camera.main.transform.position) && !Physics.CheckSphere(pos, colliderRange) && !Physics.Linecast(transform.position, pos))
            {
                print("Leaving Cover");
                state = "move";
                return pos;
            }
        }

        //return zero if no new position is found after 100 tries
        Debug.Log("Could not find new location to move", gameObject);
        return Vector3.zero;
    }

    private void OnDrawGizmosSelected()
    {
        Color c = Color.red;
        c.a = 0.5f;
        Gizmos.color = c;
        if (Application.isPlaying)
        {
            Gizmos.DrawSphere(startPos, coverRange);
        }
        else
        {
            Gizmos.DrawSphere(transform.position, coverRange);
        }

        c = Color.blue;
        c.a = 0.5f;
        Gizmos.color = c;
        Gizmos.DrawSphere(transform.position, colliderRange);
    }
}
