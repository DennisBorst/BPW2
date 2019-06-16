using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class BomberEnemy : Enemy
{
    private Animator anim;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();

        if (slowed == false)
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
                transform.position = Vector3.MoveTowards(transform.position, objective.position, speed * Time.deltaTime);
            }
            else
            {
                anim.SetTrigger("bomb");
            }
        }
    }

    public override void CheckDistance()
    {
        base.CheckDistance();
    }

    public void Bomb()
    {
        objective.GetComponent<Objective>().DamageTaken(damage);
        AttackObjective();
        ParticleManager.instance.SpawnParticle(ParticleManager.instance.hitParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}