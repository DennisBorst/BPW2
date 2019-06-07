using System.Collections;
using System.Collections.Generic;
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
        //Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Colliding");
        if (collision.tag == "Objective")
        {
            ParticleManager.instance.SpawnParticle(ParticleManager.instance.hitParticle, transform.position, transform.rotation);
            target.GetComponent<Objective>().DamageTaken(damage);
            DestroyProjectile();
        }

        if (collision.tag == "Projectiles")
        {
            ParticleManager.instance.SpawnParticle(ParticleManager.instance.fireHitParticle, transform.position, transform.rotation);
            DestroyProjectile();
        }
        Debug.Log(collision.gameObject.tag);
    }
}
