using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundCoverState : GroundEnemyState
{
    public Vector3 newPos;
    private float time;

    public override void OnStateEnter(GroundEnemySC enemy)
    {
        for (int i = 0; i < 100; i++)
        {
            //choose a random point in range
            Vector3 pos = (Random.insideUnitSphere * enemy.moveRange) + enemy.startPos;

            //check if there will be an object between the enemy and player
            if (Physics.Linecast(pos, Camera.main.transform.position, enemy.castLayers))
            {
                newPos = pos;
                enemy.MoveToLocation(newPos);

                return;
            }
        }

    }

    public override void Act(GroundEnemySC enemy)
    {
        time += Time.deltaTime;

        if (time > enemy.navigationTimeout || Vector3.Distance(enemy.agent.destination, enemy.transform.position) <= enemy.distanceReach)
        {
            enemy.agent.isStopped = true;
            enemy.SetState(new GroundIdleState());
        }
    }
}
