using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject enemyBullet;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float stopDistance;
    [SerializeField] private float attackTime;

    private void Update()
    {
        CloseDistance();
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
                    //StartCoroutine(Attack());
                    //objective.GetComponent<Objective>().DamageTaken(damage);
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
