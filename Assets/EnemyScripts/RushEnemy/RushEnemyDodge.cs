using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RushEnemyDodge : RushEnemyState
{
    private Vector3 pos;
    private Vector3 initPos;  
    private float moveDistance = 1f; 

    public override void OnStateEnter(RushEnemySC enemy)
    {//picks random direction and moves there
        var startPos = enemy.agent.transform;
        initPos = startPos.position; 

        float rand = Random.Range(0, 1); 
        if(rand > 0.5f)
        {
            pos = initPos + startPos.right * moveDistance;  
        } 
        else
        {
            pos = initPos - startPos.right * moveDistance;
        }

        enemy.agent.destination = pos; 
    }

    public override void Act(RushEnemySC enemy)
    {
      
        if (enemy.agent.remainingDistance <= enemy.agent.stoppingDistance)
        {
            
            enemy.SetState(new RushMoveState());
        }
    }
}
