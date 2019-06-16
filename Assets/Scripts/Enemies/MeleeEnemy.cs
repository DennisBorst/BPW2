using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private bool meleeEnemy;

    private float attackTime;
    private Animator anim;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();
        if(slowed == false)
        {
            CheckDistance();
            CloseDistance();
        }
    }
    
    void CloseDistance()
    {
        if (objective != null)
        {
            if (!destinationReached)
            {
                //if (Vector3.Distance(transform.position, objective.position) > stopDistance)
                transform.position = Vector3.MoveTowards(transform.position, objective.position, speed * Time.deltaTime);
            }
            else
            {
                if (Time.time >= attackTime)
                {
                    if (meleeEnemy)
                    {
                        anim.SetTrigger("attack");
                    }
                    else
                    {
                        anim.SetTrigger("attack2");
                    }
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

    public void Attack()
    {
        if(objective != null)
        {
            objective.GetComponent<Objective>().DamageTaken(damage);
            AttackObjective();
        } 
    }

    public override void CheckDistance()
    {
        base.CheckDistance();
    }
}

