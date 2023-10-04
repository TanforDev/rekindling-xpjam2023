using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Animations : MonoBehaviour
{
    private Enemy_Manager enemy_Manager;
    private Animator animator;
    private void OnEnable()
    {
        SetInitialReferences();
        enemy_Manager.OnEnemyAttack += OnAttack;
        enemy_Manager.OnEnemyDamage += OnDamage;
        enemy_Manager.OnEnemyReleaseAttack += OnRelease;
    }
    private void OnDisable()
    {
        enemy_Manager.OnEnemyAttack -= OnAttack;
        enemy_Manager.OnEnemyDamage -= OnDamage;
        enemy_Manager.OnEnemyReleaseAttack -= OnRelease;
    }

    private void SetInitialReferences()
    {
        enemy_Manager = GetComponent<Enemy_Manager>();
        animator = GetComponent<Animator>();
        animator.CrossFade("Idle", 0);
    }

    private void OnAttack()
    {
        animator.CrossFade("Attack", 0);
    }

    private void OnDamage(int dummy)
    {
        animator.CrossFade("Damage", 0);
    }

    private void OnRelease()
    {
        animator.CrossFade("Idle", 0);
    }
}
