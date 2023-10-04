using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Audio : MonoBehaviour
{
    [SerializeField] private AudioClip damage;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip win;

    private Player_Manager pm;
    private void OnEnable()
    {
        SetInitialReferences();
        pm.OnPlayerDead += Death;
        pm.OnDamageByEnemy += Damage;
        pm.OnPlayerWin += Win;
    }

    private void OnDisable()
    {
        pm.OnPlayerDead -= Death;
        pm.OnDamageByEnemy -= Damage;
        pm.OnPlayerWin -= Win;
    }

    private void SetInitialReferences()
    {
        pm = GetComponent<Player_Manager>();
    }

    private void Death()
    {
        SoundMananger.Instance.PlayClip(death, transform.position);
    }

    private void Damage(int dummy)
    {
        SoundMananger.Instance.PlayClip(damage, transform.position);
    }

    private void Win()
    {
        SoundMananger.Instance.PlayClip(win, transform.position);
    }
}
