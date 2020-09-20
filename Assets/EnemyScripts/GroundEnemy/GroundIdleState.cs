using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundIdleState : GroundEnemyState
{
    private float time;

    public override void OnStateEnter(GroundEnemySC enemy)
    {
        enemy.canDodge = false;
    }

    public override void Act(GroundEnemySC enemy)
    {
        if (enemy.canDodge == true)
        {
            Debug.Log("try dodge");
            int rand = Random.Range(1, 101);
            if (rand <= enemy.dodgeChance)
            {
                Debug.Log("Dodge");
                enemy.SetState(new GroundDodgeState());
            }
            else
            {
                enemy.canDodge = false;
            }
        }

        time += Time.deltaTime;

        if (time >= enemy.waitTime)
        {
            time = 0;

            //check if the enemy is behind cover
            if (Physics.Linecast(enemy.transform.position, Camera.main.transform.position, enemy.castLayers))
            {
                enemy.SetState(new GroundUncoverState());
            }
            else
            {
                //random chance of shooting when done waiting
                int rand = Random.Range(1, 100);
                //also make sure the enemy is visible when it shoots
                if (rand <= enemy.chanceToAttack)
                {
                    enemy.Shoot();
                }

                enemy.SetState(new GroundCoverState());
            }
        }
    }
}
