using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerEnemy : Enemy
{
    [SerializeField] private float stopDistance;
    [SerializeField] private float summonTime;

    [Space]
    public GameObject summonEnemy;

    private void Update()
    {
        CloseDistance();
    }

    void CloseDistance()
    {
        if (objective != null)
        {
            if (Vector3.Distance(transform.position, objective.position) > stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, objective.position, speed * Time.deltaTime);
            }
            else
            {
                if (Time.time >= summonTime)
                {
                    Summon();
                    summonTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

    public void Summon()
    {
        if (objective != null)
        {
            Instantiate(summonEnemy, transform.position, transform.rotation);
        }
    }
}
