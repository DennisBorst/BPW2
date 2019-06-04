using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MeleeEnemy : Enemy
{
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
                    objective.GetComponent<Objective>().DamageTaken(damage);
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

    /*
    IEnumerator Attack()
    {
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }*/

}

