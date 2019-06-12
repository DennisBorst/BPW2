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
    private float paralyzedSpeed;
    private float defaultSpeed;
    public float stopDistance;
    public bool destinationReached = false;
    public float maxDistanceRaycast;
    public LayerMask mask;

    [Header("Frozen Info")]
    public bool slowed = false;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material frozen;
    [SerializeField] private Material paralyzed;
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
        paralyzedSpeed = defaultSpeed * 2f;
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

    public virtual void CheckDistance()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        Ray ray = new Ray(position, transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, maxDistanceRaycast, mask))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
            destinationReached = true;
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * maxDistanceRaycast, Color.green);
        }
    }

    public void StartFreeze(float slowStrength, float slowDuration, bool iceArrow, float paralyzedMovement)
    {
        if (freezeRoutine != null) StopCoroutine(freezeRoutine);
        freezeRoutine = StartCoroutine(IFreeze(slowStrength, slowDuration, iceArrow, paralyzedMovement));
    }

    IEnumerator IFreeze(float slowStrength, float slowDuration, bool iceArrow, float paralyzedMovement)
    {
        if (iceArrow)
        {
            slowed = true;
            normalColor.material = frozen;
            Debug.Log("SLOWED");
            speed *= slowStrength;
        }
        else
        {
            normalColor.material = paralyzed;
            Debug.Log("PARALYZED");
            speed = (speed * slowStrength) - paralyzedSpeed;
            Debug.Log(speed);
        }
        
        yield return new WaitForSeconds(slowDuration);
        normalColor.material = defaultMaterial;
        speed = defaultSpeed;
        slowed = false;
    }
}