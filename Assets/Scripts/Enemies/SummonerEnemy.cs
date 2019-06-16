using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerEnemy : Enemy
{

    [Space]
    [Header("Summon")]
    public GameObject summonEnemy;
    private float summonTime;

    [SerializeField] private Transform spawn;

    
    private Animator anim;

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
            CloseDistance();
        }
    }

    void CloseDistance()
    {
        if (objective != null)
        {
            if (Vector3.Distance(transform.position, objective.position) > stopDistance)
            {
                Debug.Log("Moving");
                transform.position = Vector3.MoveTowards(transform.position, objective.position, speed * Time.deltaTime);
                //anim.SetBool("isFloating", true);
            }
            else
            {
                if (Time.time >= summonTime)
                {
                    //anim.SetBool("isFloating", false);
                    //Summon();
                    anim.SetTrigger("summon");
                    summonTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

    public void Summon()
    {
        if (objective != null)
        {
            Instantiate(summonEnemy, spawn.position, transform.rotation);
        }
    }

    void SmokeParticles()
    {
        ParticleManager.instance.SpawnParticle(ParticleManager.instance.smokeParticles, transform.position, transform.rotation);
    }
}
