using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth = 9;
    [SerializeField] private float regenerationRate = 2;

    [SerializeField] private MMF_Player hitImpulse;
    [SerializeField] private float invTime = 3;
    private float hitTime;
    public int Health => health;
    public int MaxHealth => maxHealth;

    private float currentTime;

    private Player_Manager playerManager;

    public delegate void HealthEventHandler(int currentHealth);
    public event HealthEventHandler OnHealthUpdated;

    private void OnEnable()
    {
        SetInitialReferences();
        playerManager.OnHealth += HealthCheck;
        playerManager.OnDamageByEnemy += HittedByEnemy;
    }

    private void OnDisable()
    {
        playerManager.OnHealth -= HealthCheck;
        playerManager.OnDamageByEnemy -= HittedByEnemy;
    }

    private void SetInitialReferences()
    {
        playerManager = GetComponent<Player_Manager>();
        health = maxHealth;
        HealthCheck(0);
    }

    private void HittedByEnemy(int damage)
    {
        if(hitTime < Time.time)
        {
            hitImpulse.PlayFeedbacks();
            HealthCheck(damage);
            hitTime = Time.time + invTime;
        }
    }

    private void HealthCheck(int points)
    {
        health += points;
        OnHealthUpdated?.Invoke(health);
        if(health <= 0)
        {
            playerManager.CallOnPlayerDead();
        }
        currentTime = Time.time + regenerationRate;
    }

    private void Update()
    {
        if(health < maxHealth)
        {
            if(currentTime < Time.time)
            {
                playerManager.AddHealth(1);
                currentTime = Time.time + regenerationRate;
            }
        }
    }
}
