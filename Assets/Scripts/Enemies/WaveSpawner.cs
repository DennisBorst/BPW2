using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class WaveSpawner : MonoBehaviour {

    //[Header("UI")]
    //[SerializeField] private TextMeshProUGUI nextWaveText;
    //[SerializeField] private Animator anim;
    //[SerializeField] private Animator animFade;
    [SerializeField] private Objective objectiveScript;
    [Range(0,1)]
    [SerializeField] private float healthPercentage;

    [SerializeField] private Animator anim;

    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform objective;

    private bool finishSpawning;
    private bool startParticle = false;

    private void Start()
    {
        objective = GameObject.FindGameObjectWithTag("Objective").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        if (index != 0)
        {
            ParticleManager.instance.SpawnParticle(ParticleManager.instance.startRingParticle, transform.position, transform.rotation);
            Camera.main.transform.DOComplete();
            Camera.main.transform.DOShakePosition(2f, 1f, 10, 90, false, true);

            objectiveScript.currentHealth += (healthPercentage * objectiveScript.maxHealth);
        }
        
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];

        for(int i = 0; i < currentWave.count; i++)
        {
            if (objective == null)
            {
                yield break;
            }

            if(i == currentWave.count - 1)
            {
                finishSpawning = true;
            }
            else
            {
                finishSpawning = false;
            }

            Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);
            
            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }

    private void Update()
    {
        if(finishSpawning == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finishSpawning = false;
            if(currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex++;
                //anim.enabled = true;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                anim.SetTrigger("FadeOut");
                Debug.Log("Game has finished");
                //animFade.SetTrigger("FadeOut");
            }
        }
    }
}
