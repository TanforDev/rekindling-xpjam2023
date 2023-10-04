using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Audio : MonoBehaviour
{
    [SerializeField] private AudioClip[] flaps;
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip laugh;
    [SerializeField] private AudioClip[] damage;
    [SerializeField] private AudioClip death;

    private Enemy_Manager enemy_Manager;
    private bool isFlapping;

    private void OnEnable()
    {
        SetInitialReferences();
        enemy_Manager.OnEnemyAttack += OnAttack;
        enemy_Manager.OnEnemyDamage += OnDamage;
        enemy_Manager.OnEnemyDie += OnDie;
    }

    private void OnDisable()
    {
        enemy_Manager.OnEnemyAttack -= OnAttack;
        enemy_Manager.OnEnemyDamage -= OnDamage;
        enemy_Manager.OnEnemyDie -= OnDie;
    }

    private void SetInitialReferences()
    {
        enemy_Manager = GetComponent<Enemy_Manager>();
    }

    private void OnAttack()
    {
        SoundMananger.Instance.PlayClip(attack, transform.position);
    }

    private void OnDamage(int dummy)
    {
        SoundMananger.Instance.PlayClip(damage[Random.Range(0, damage.Length)], transform.position);
    }

    private void OnDie()
    {
        SoundMananger.Instance.PlayClip(death, transform.position);
    }

    private void Update()
    {
        if(enemy_Manager.EnemyState == EnemyState.Idle && isFlapping == false)
        {
            StartCoroutine(DoFlapSound());
            isFlapping = true;
        }

        if(isFlapping && enemy_Manager.EnemyState != EnemyState.Idle)
        {
            StopAllCoroutines();
            isFlapping = false;
        }
    }

    private IEnumerator DoFlapSound()
    {
        while (true)
        {
            AudioSource.PlayClipAtPoint(flaps[Random.Range(0, flaps.Length)], transform.position);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
