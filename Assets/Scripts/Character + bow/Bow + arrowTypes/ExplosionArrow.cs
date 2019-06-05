using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExplosionArrow : MonoBehaviour
{
    public int damage;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private SphereCollider explosionCollider;
    [SerializeField] private float explosionTime;

    [Header("Particles")]
    //public GameObject hitParticle;
    public ParticleSystem trailParticle;

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
                collision.rigidbody.GetComponent<Rigidbody>().isKinematic = true;
                collision.gameObject.GetComponent<Enemy>().DamageTaken(damage);
            }

            explosionCollider.enabled = true;
            rb.isKinematic = true;
            rb.useGravity = false;

            ParticleManager.instance.SpawnParticle(ParticleManager.instance.fireHitParticle, transform.position, transform.rotation);
            trailParticle.transform.parent = transform.parent;
            trailParticle.Stop();

            Camera.main.transform.DOComplete();
            Camera.main.transform.DOShakePosition(.4f, .5f, 20, 90, false, true);

            StartCoroutine(Explosion());
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
                other.gameObject.GetComponent<Enemy>().DamageTaken(damage / 2);
            }
        }
    }

    IEnumerator Explosion()
    {
        explosionCollider.enabled = true;
        yield return new WaitForSeconds(explosionTime);
        explosionCollider.enabled = false;
        Destroy(gameObject);
    }
}
