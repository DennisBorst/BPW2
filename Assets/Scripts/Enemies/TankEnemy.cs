using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class TankEnemy : Enemy
{

    private float attackTime;

    [Space]

    [Header("Shield")]
    [SerializeField] private GameObject shieldObject;
    [SerializeField] private float shieldUpTime;
    [SerializeField] private float shieldCooldown;
    private float currentShieldCooldown;

    private Animator anim;

    /*
    public enum AI_State
    {
        walking,
        shielding,
    }
    */

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
            currentShieldCooldown += Time.deltaTime;
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
                if (Time.time >= attackTime)
                {
                    //StartCoroutine(Attack());
                    anim.SetTrigger("sword_attack");
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }

            if (shieldCooldown < currentShieldCooldown)
            {
                currentShieldCooldown = 0 - shieldUpTime;
                StartCoroutine(Shield());
                //shieldCooldown = currentShieldCooldown;
            }
        }
    }

    public override void CheckDistance()
    {
        base.CheckDistance();
    }

    IEnumerator Shield()
    {
        shieldObject.SetActive(true);
        yield return new WaitForSeconds(shieldUpTime);
        shieldObject.SetActive(false);
    }

    public void SwordAttack()
    {
        objective.GetComponent<Objective>().DamageTaken(damage);
        AttackObjective();
    }

}


