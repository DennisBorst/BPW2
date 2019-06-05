using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;

    [Header("Character particles")]
    public ParticleGroup fireHitParticle;
    public ParticleGroup iceHitParticle;
    public ParticleGroup forceHitParticle;
    public ParticleGroup pierceHitParticle;

    [Header("Enemy Explosions")]
    public ParticleGroup hitParticle;



    void Awake()
    {
        instance = this;

        InitiatePool(fireHitParticle);
        InitiatePool(iceHitParticle);
        InitiatePool(forceHitParticle);
        InitiatePool(pierceHitParticle);

        InitiatePool(hitParticle);

    }

    public void InitiatePool(ParticleGroup particle)
    {
        PoolManager.instance.CreatePool(particle.particlePrefab, particle.amount);
    }

    public GameObject SpawnParticle(ParticleGroup particle, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        GameObject obj = PoolManager.instance.ReuseObject(particle.particlePrefab, spawnPosition, spawnRotation);
        if (obj.GetComponent<ParticleExplosion>())
        {
            obj.GetComponent<ParticleExplosion>().Explode();
        }
        return obj;
    }
}
[System.Serializable]
public class ParticleGroup: System.Object
{
    public GameObject particlePrefab;
    public int amount;
}
