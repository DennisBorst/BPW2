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

    /*
    public enum AI_State
    {
        walking,
        shielding,
    }
    */

    public override void Update()
    {
        base.Update();

        if (slowed == false)
        {
            currentShieldCooldown += (1f / 60f);
            CheckDistance();
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
                    //StartCoroutine(Attack());
                    objective.GetComponent<Objective>().DamageTaken(damage);
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
}


