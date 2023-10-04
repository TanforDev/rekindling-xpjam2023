using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    [SerializeField] private EnemyState enemyState;
    public EnemyState EnemyState
    {
        get
        {
            return enemyState;
        }
        set
        {
            enemyState = value;
        }
    }

    [SerializeField] private Transform player;
    public Transform Player => player;

    public delegate void DetectPlayerHandler(Transform player);
    public event DetectPlayerHandler OnDetectPlayer;
    public event DetectPlayerHandler OnLostPlayer;

    public delegate void EnemyHealthHandler(int health);
    public event EnemyHealthHandler OnEnemyDamage;

    public delegate void GeneralEventHandler();
    public event GeneralEventHandler OnEnemyDie;
    public event GeneralEventHandler OnEnemyChargeAttack;
    public event GeneralEventHandler OnEnemyAttack;
    public event GeneralEventHandler OnEnemyReleaseAttack;
    public void CallDetectPlayer(Transform player)
    {
        this.player = player;
        OnDetectPlayer?.Invoke(player);
    }

    public void DamageEnemy(int damage)
    {
        if(enemyState != EnemyState.ChargeAttack && enemyState != EnemyState.Attacking)
        {
            enemyState = EnemyState.Hurt;
        }

        OnEnemyDamage?.Invoke(damage);
    }

    public void CallOnEnemyDead()
    {
        OnEnemyDie?.Invoke();
    }

    public void CallOnEnemyChargeAttack()
    {
        if (enemyState == EnemyState.ChargeAttack || enemyState == EnemyState.Attacking) return;
        OnEnemyChargeAttack?.Invoke();
    }
    public void CallOnEnemyAttack()
    {
        enemyState = EnemyState.Attacking;
        OnEnemyAttack?.Invoke();
    }

    public void CallOnAttackEnd()
    {
        enemyState = EnemyState.Idle;
        OnEnemyReleaseAttack?.Invoke();
    }
}

public enum EnemyState
{
    Idle,
    ChargeAttack,
    Attacking,
    Hurt
}
