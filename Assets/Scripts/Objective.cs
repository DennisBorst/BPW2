using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private GameManager gameManager;

    [Header("UI")]
    public Slider[] hpBar;
    public Image bloodPanel;
    private float speedUI = 5f;

    [Header("Game stats")]
    public float maxHealth;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;

        for (int i = 0; i < hpBar.Length; i++)
        {
            hpBar[i].maxValue = currentHealth;
            hpBar[i].value = currentHealth;
        }
        

        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hpBar.Length; i++)
        {
            hpBar[i].value = currentHealth;
        }
    }

    public void DamageTaken(int damageAmount)
    {
        currentHealth -= damageAmount;
        StartCoroutine(BloodUI());

        if (currentHealth <= 0)
        {
            ParticleManager.instance.SpawnParticle(ParticleManager.instance.deathBusParticle, transform.position - transform.up * 2f, transform.rotation);
            Camera.main.transform.DOComplete();
            Camera.main.transform.DOShakePosition(2f, 1f, 10, 90, false, true);
            Destroy(gameObject);
            gameManager.ReloadScene();
        }
    }

    IEnumerator BloodUI()
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * speedUI;
            bloodPanel.color = new Color(255f, 255f, 255f, t);
            yield return 0;
        }
        while (t > 0f)
        {
            t -= Time.deltaTime * speedUI;
            bloodPanel.color = new Color(255f, 255f, 255f, t);
            yield return 0;
        }
    }
}
