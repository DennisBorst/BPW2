using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{

    [Header("Ranged")]
    public GameObject enemyBullet;
    [SerializeField] private Transform shootPoint;
    private float attackTime;

    public override void Update()
    {
        base.Update();

        if (slowed == false)
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
                    RangedAttack();
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

    public void RangedAttack()
    {
        if (objective != null)
        {
            Instantiate(enemyBullet, shootPoint.position, transform.rotation);
        }
    }
}
