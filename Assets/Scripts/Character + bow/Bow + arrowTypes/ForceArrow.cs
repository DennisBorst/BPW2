using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ForceArrow : MonoBehaviour
{
    public int damage;
    [SerializeField] private float force;
    private GameObject enemy;
    private bool stopForce = false;

    [Header("Particles")]
    //public GameObject hitParticle;
    public ParticleSystem trailParticle;

    private void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyShield")
        {
            Destroy(gameObject);
        }
        else
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.rigidbody.isKinematic = false;
                Vector3 localForce = transform.forward * force;
                collision.gameObject.GetComponent<Enemy>().DamageTaken(damage);
                collision.rigidbody.GetComponent<Rigidbody>().AddForce(localForce);
                Destroy(gameObject);
            }
            ParticleManager.instance.SpawnParticle(ParticleManager.instance.forceHitParticle, transform.position, transform.rotation);
            trailParticle.transform.parent = transform.parent;
            trailParticle.Stop();

            Camera.main.transform.DOComplete();
            Camera.main.transform.DOShakePosition(.4f, .5f, 20, 90, false, true);

            Destroy(trailParticle.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyShield")
        {
            Destroy(gameObject);
        }
        else
        {
            if (other.gameObject.tag == "Enemy")
            {
                Destroy(gameObject);
            }
        }
    }
}
