using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth = 9;

    [SerializeField] private MMF_Player hitImpulse;
    public int Health => health;
    public int MaxHealth => maxHealth;

    private Enemy_Manager enemy_Manager;

    public delegate void HealthEventHandler(int currentHealth);
    public event HealthEventHandler OnHealthUpdated;

    bool isDead;

    private void OnEnable()
    {
        SetInitialReferences();
        enemy_Manager.OnEnemyDamage += HealthCheck;
    }

    private void OnDisable()
    {
        enemy_Manager.OnEnemyDamage -= HealthCheck;
    }

    private void SetInitialReferences()
    {
        enemy_Manager = GetComponent<Enemy_Manager>();
        health = maxHealth;
        HealthCheck(0);
    }
    private void HealthCheck(int points)
    {
        if (isDead) return;
        health -= points;
        hitImpulse.PlayFeedbacks();
        OnHealthUpdated?.Invoke(health);
        if (health <= 0)
        {
            enemy_Manager.CallOnEnemyDead();
            isDead = true;
        }
    }
}
