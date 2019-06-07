using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    private float attackTime;

    public override void Update()
    {
        base.Update();
        if(slowed == false)
        {
            CloseDistance();
        }
    }
    
    void CloseDistance()
    {
        if (objective != null)
        {
            if (Vector3.Distance(transform.position, objective.position) > stopDistance)
                transform.position = Vector3.MoveTowards(transform.position, objective.position, speed * Time.deltaTime);
            else
            {
                if (Time.time >= attackTime)
                {
                    objective.GetComponent<Objective>().DamageTaken(damage);
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }
}

