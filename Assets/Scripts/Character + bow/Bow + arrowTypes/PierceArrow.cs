using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PierceArrow : MonoBehaviour
{
    public int damage;

    [Header("Particles")]
    //public GameObject hitParticle;
    public ParticleSystem trailParticle;

    public void OnTriggerEnter(Collider other)
    {
        ParticleManager.instance.SpawnParticle(ParticleManager.instance.pierceHitParticle, transform.position, transform.rotation);
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().DamageTaken(damage);
        }
        else if (other.gameObject.tag == "Ground")
        {
            trailParticle.transform.parent = transform.parent;
            trailParticle.Stop();

            Camera.main.transform.DOComplete();
            Camera.main.transform.DOShakePosition(.4f, .5f, 20, 90, false, true);

            Destroy(trailParticle.gameObject);
            Destroy(gameObject);
        }
    }
}
