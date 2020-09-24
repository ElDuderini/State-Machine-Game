using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RushMoveState : RushEnemyState
{
    public Vector3 newPos;
    private float time;

    public override void OnStateEnter(RushEnemySC enemy)
    {
        //set navmesh agent's destination as the player
        enemy.agent.destination = Camera.main.transform.position;
    }

    public override void Act(RushEnemySC enemy)
    {
        time += Time.deltaTime;

        //if close enough to the player, explode
        if(enemy.agent.remainingDistance <= enemy.agent.stoppingDistance)
        {
            enemy.agent.isStopped = true;
            enemy.SetState(new RushAttackState());
        }
    }
}
