using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class BomberEnemy : Enemy
{
    [SerializeField] private float stopDistance;

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
                objective.GetComponent<Objective>().DamageTaken(damage);
                ParticleManager.instance.SpawnParticle(ParticleManager.instance.hitParticle, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}