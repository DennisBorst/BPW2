using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class TeleportEnemy : Enemy
{

    private float attackTime;

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
    [SerializeField] AI_State currentState;

    public enum AI_State
    {
        walking,
        shooting,
        teleporting
    }

    
    void SwitchState()
    {
        switch (currentState)
        {
            case AI_State.walking:
                CloseDistance();
                break;

            case AI_State.shooting:
                RangedAttack();
                break;

            case AI_State.teleporting:
                Teleport();
                break;
        }
    }
    */

    Vector3 pos;

    public override void Update()
    {
        base.Update();

        if (slowed == false)
        {
            currentTeleportCooldown += (1f / 60f);
            CloseDistance();
        }
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
            Vector3 throwingStar = new Vector3(transform.rotation.x + 90, transform.rotation.y, transform.rotation.z);
            Instantiate(enemyBullet, shootPoint.position, enemyBullet.transform.rotation);
        }
    }

    void Teleport()
    {
        pos = new Vector3(
            Random.Range(objective.position.x - stopDistance - objectiveSize, objective.position.x + stopDistance + objectiveSize), 0,
            Random.Range(-objective.position.z - stopDistance - objectiveSize, objective.position.z + stopDistance + objectiveSize));
        transform.position = pos;
        teleportAvailable = true;
    }
}
