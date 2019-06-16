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

    private Animator anim;

    Vector3 pos;


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
            currentTeleportCooldown += Time.deltaTime;
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
                anim.SetTrigger("teleport");
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

    void SmokeParticles()
    {
        ParticleManager.instance.SpawnParticle(ParticleManager.instance.smokeParticles, transform.position, transform.rotation);
    }
}
