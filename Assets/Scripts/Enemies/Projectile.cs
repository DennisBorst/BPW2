using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public float lifeTime;
    //public GameObject explosion;
    public int damage;

    private Transform target;

    // Use this for initialization
    private void Start()
    {
        target = ObjectiveLocation.Instance.objective.transform;
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y + 1.5f, target.position.z), speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag != "Enemy")
        {
            if (collision.tag == "Objective")
            {
                ParticleManager.instance.SpawnParticle(ParticleManager.instance.hitParticle, transform.position, transform.rotation);
                target.GetComponent<Objective>().DamageTaken(damage);
                Camera.main.transform.DOComplete();
                Camera.main.transform.DOShakePosition(.4f, .5f, 20, 90, false, true);
                DestroyProjectile();
            }

            if (collision.tag == "Projectiles")
            {
                ParticleManager.instance.SpawnParticle(ParticleManager.instance.fireHitParticle, transform.position, transform.rotation);
                DestroyProjectile();
            }
        }
    }
}
