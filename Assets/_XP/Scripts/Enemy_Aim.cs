using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Aim : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    private Enemy_Manager enemy_Manager;

    private void OnEnable()
    {
        SetInitialReferences();
    }

    private void SetInitialReferences()
    {
        enemy_Manager = GetComponent<Enemy_Manager>();
    }
    private void Update()
    {
        if (enemy_Manager.Player == null) return;
        if (enemy_Manager.EnemyState == EnemyState.Attacking) return;

        Vector2 dir = enemy_Manager.Player.position - transform.position;
        sprite.flipX = dir.x < 0;

    }
}
