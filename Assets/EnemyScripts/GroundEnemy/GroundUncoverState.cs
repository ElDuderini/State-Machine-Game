using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundUncoverState : GroundEnemyState
{
    public Vector3 newPos;
    private float time;

    public override void OnStateEnter(GroundEnemySC enemy)
    {
        for (int i = 0; i < 100; i++)
        {
            //choose a random point in range
            Vector3 pos = (Random.insideUnitSphere * enemy.moveRange) + enemy.startPos;
            pos.y = 0;

            //check if there will not be an object between the enemy and player
            if (!Physics.Linecast(pos, Camera.main.transform.position, enemy.castLayers))
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

        if(time > enemy.navigationTimeout || enemy.agent.remainingDistance <= enemy.agent.stoppingDistance)
        {
            enemy.agent.isStopped = true;
            enemy.SetState(new GroundIdleState());
        }
    }
}
