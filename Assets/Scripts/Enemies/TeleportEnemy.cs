using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class TeleportEnemy : Enemy
{
    [SerializeField] private float stopDistance;
    [SerializeField] private float attackTime;

    [Header("Bullet")]
    public GameObject enemyBullet;
    [SerializeField] private Transform shootPoint;

    [Space]

    [Header("Teleport")]
    //[SerializeField] private float teleportUpTime;
    [SerializeField] private float objectiveSize;
    [SerializeField] private float teleportCooldown;
    private float currentTeleportCooldown;
    private bool teleportAvailable = false;

    /*
    public enum AI_State
    {
        walking,
        shielding,
    }
    */

    Vector3 pos;

    private void Update()
    {
        currentTeleportCooldown += (1f / 60f);
        CloseDistance();
        RandomPos();
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
                if (Time.time >= attackTime)
                {
                    teleportAvailable = true;
                    //StartCoroutine(Attack());
                    RangedAttack();
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }

            if (teleportCooldown < currentTeleportCooldown && teleportAvailable == true)
            {
                currentTeleportCooldown = 0;
                Teleport();
                teleportAvailable = false;
                //shieldCooldown = currentShieldCooldown;
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

    void Teleport()
    {
        transform.position = pos;
        teleportAvailable = true;
    }

    void RandomPos()
    {
        pos = new Vector3(Random.Range(objective.position.x - stopDistance - objectiveSize , objective.position.x + stopDistance + objectiveSize), 0, Random.Range(-objective.position.z - stopDistance - objectiveSize, objective.position.z + stopDistance + objectiveSize));
    }
}
