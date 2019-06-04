using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [HideInInspector]
    public Transform objective;
    private bool isDead = false;

    [Header("UI")]
    public Slider hpBar;

    //[Header("Particles")]
    //public GameObject deathParticles;

    [Header("main stats")]
    public float health;
    public float speed;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    public int damage;

    public virtual void Start()
    {
        objective = ObjectiveLocation.Instance.objective.transform;
        hpBar.maxValue = health;
        hpBar.value = health;
    }

    public void DamageTaken(int damageAmount)
    {
        hpBar.value -= damageAmount;
        if (hpBar.value <= 0 && !isDead)
        {
            isDead = true;
            //Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}