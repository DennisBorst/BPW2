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
    private float currentSpeed;
    public bool slowed = false;
    [SerializeField] private Material frozen;
    private MeshRenderer normalColor;
    private int currentSlowTime = 0;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    public int damage;

    public virtual void Start()
    {
        currentSpeed = speed;
        objective = ObjectiveLocation.Instance.objective.transform;
        hpBar.maxValue = health;
        hpBar.value = health;
        normalColor = GetComponentInChildren<MeshRenderer>();
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

    public void Freeze(float slowStrength, int slowDuration)
    {
        if(slowed == false)
        {
            slowed = true;
            normalColor.material = frozen;
            Debug.Log("SLOWED");
            speed *= slowStrength;
        }
  
    }
}