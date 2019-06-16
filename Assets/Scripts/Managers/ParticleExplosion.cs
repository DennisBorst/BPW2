using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleExplosion : PoolObject
{
    [SerializeField] private bool sound = false;

    // Start is called before the first frame update
    public void Explode()
    {
        GetComponent<ParticleSystem>().Play();
        if (sound)
        {
            GetComponent<AudioSource>().Play();
        }
        StartCoroutine(CheckingIfRunning());
    }
    IEnumerator CheckingIfRunning()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        
        if (!sound)
        {
            while (ps.IsAlive())
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        else if (sound)
        {
            AudioSource source = GetComponent<AudioSource>();
            while (ps.IsAlive() && source.isPlaying)
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        Destroy();
    }
}
