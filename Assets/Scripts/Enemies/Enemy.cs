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
    private float defaultSpeed;
    public float stopDistance;

    [Header("Frozen Info")]
    public bool slowed = false;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material frozen;
    private MeshRenderer normalColor;
    Coroutine freezeRoutine;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    public int damage;
    

    public virtual void Start()
    {
        objective = ObjectiveLocation.Instance.objective.transform;
        hpBar.maxValue = health;
        hpBar.value = health;

        //Freeze
        defaultSpeed = speed;
        normalColor = GetComponentInChildren<MeshRenderer>();
    }

    public virtual void Update()
    {
        gameObject.transform.LookAt(objective);
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

    public void StartFreeze(float slowStrength, float slowDuration)
    {
        if (freezeRoutine != null) StopCoroutine(freezeRoutine);
        freezeRoutine = StartCoroutine(IFreeze(slowStrength, slowDuration));
    }

    IEnumerator IFreeze(float slowStrength, float slowDuration)
    {
        slowed = true;
        normalColor.material = frozen;
        Debug.Log("SLOWED");
        speed *= slowStrength;
        yield return new WaitForSeconds(slowDuration);
        normalColor.material = defaultMaterial;
        speed = defaultSpeed;
        slowed = false;
    }
}