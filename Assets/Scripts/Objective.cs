using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private GameManager gameManager;

    [Header("UI")]
    public Slider hpBar;

    [Header("Game stats")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        hpBar.maxValue = currentHealth;
        hpBar.value = currentHealth;

        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = currentHealth;
    }

    public void DamageTaken(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            gameManager.ReloadScene();
        }
    }
}
