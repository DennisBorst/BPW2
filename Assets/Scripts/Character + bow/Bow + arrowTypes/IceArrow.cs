using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IceArrow : MonoBehaviour
{
    public int damage;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private SphereCollider iceCollider;
    [SerializeField] private float iceAreaTime;
    [SerializeField] private int slowDuration;
    [Range(1, 0)]
    [SerializeField] private float slowStrength;

    [Header("Particles")]
    //public GameObject hitParticle;
    public ParticleSystem trailParticle;

    private void Update()
    {
        if (iceCollider.enabled == true)
        {

        }
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
                collision.rigidbody.GetComponent<Rigidbody>().isKinematic = true;
                collision.gameObject.GetComponent<Enemy>().DamageTaken(damage);
                //Destroy(gameObject);
            }

            
            rb.isKinematic = true;
            rb.useGravity = false;

            ParticleManager.instance.SpawnParticle(ParticleManager.instance.iceHitParticle, transform.position, transform.rotation);
            trailParticle.transform.parent = transform.parent;
            trailParticle.Stop();

            Camera.main.transform.DOComplete();
            Camera.main.transform.DOShakePosition(.4f, .5f, 20, 90, false, true);

            StartCoroutine(IceArea());
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
                other.gameObject.GetComponent<Enemy>().Freeze(slowStrength, slowDuration);
            }
        }
    }

    IEnumerator IceArea()
    {
        iceCollider.enabled = true;
        yield return new WaitForSeconds(iceAreaTime);
        iceCollider.enabled = false;
        Destroy(gameObject);
    }
}
