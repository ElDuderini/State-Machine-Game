using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDodgeState : FlyingEnemyState
{
    public Vector3 newPos;
    private int count;

    public override void OnStateEnter(FlyingEnemySC enemy)
    {
        FindNewPos(enemy);
    }

    public override void Act(FlyingEnemySC enemy)
    {
        if(newPos == Vector3.zero)
        {
            //return zero if no new position is found after 100 tries
            Debug.Log("Could not find new location with cover", enemy.gameObject);
        }
        else
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, newPos, enemy.dodgeSpeed * Time.deltaTime);
            if (enemy.transform.position == newPos)
            {
                newPos = Vector3.zero;
                count++;
                if(count < enemy.dodgeCount)
                {
                    FindNewPos(enemy);
                }
                else
                {
                    enemy.SetState(new FlyingIdleState());
                }
            }
        }
    }

    void FindNewPos(FlyingEnemySC enemy)
    {
        for (int i = 0; i < 100; i++)
        {
            //choose a random point in range
            Vector3 pos = (Random.insideUnitSphere * enemy.moveRange) + enemy.startPos;

            //check that the enemy's new position won't be inside another object, and there is no object between the enemy and new position
            if (!Physics.CheckSphere(pos, enemy.GetComponent<SphereCollider>().radius) && !Physics.Linecast(enemy.transform.position, pos, enemy.castLayers))
            {
                newPos = pos;
                return;
            }
        }
    }
}
