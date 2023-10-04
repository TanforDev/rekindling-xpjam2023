using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public delegate void GenreralEventHandler();
    public event GenreralEventHandler OnPlayerDead;
    public event GenreralEventHandler OnPlayerWin;

    public delegate void HealthEventHandler(int health);
    public event HealthEventHandler OnHealth;
    public event HealthEventHandler OnDamageByEnemy;

    public void AddHealth(int health)
    {
        OnHealth?.Invoke(health);
    }

    public void DamageHealth(int damage)
    {
        Debug.Log(damage);
        OnHealth?.Invoke(-damage);
    }
    public void HitPlayer(int damage)
    {
        Debug.Log(damage);
        OnDamageByEnemy?.Invoke(-damage);
    }

    public void CallOnPlayerDead()
    {
        OnPlayerDead?.Invoke();
    }
    public void CallOnPlayerWin()
    {
        OnPlayerWin?.Invoke();
    }
}
