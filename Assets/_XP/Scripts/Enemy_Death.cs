using DG.Tweening;
using Doozy.Engine;
using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class Enemy_Death : MonoBehaviour
{
    private Enemy_Manager enemy_Manager;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float dissolveTime = 1;

    [SerializeField] private ParticleSystem sparks;

    private Material mat;
    private Tween dissolveTween;

    private bool isDead;

    private void OnEnable()
    {
        SetInitialReferences();
        enemy_Manager.OnEnemyDie += Death;
    }

    private void OnDisable()
    {
        enemy_Manager.OnEnemyDie -= Death;
    }

    private void SetInitialReferences()
    {
        var main = sparks.main;
        main.duration = dissolveTime - 0.6f;

        enemy_Manager = GetComponent<Enemy_Manager>();
    }

    private void Death()
    {
        if (isDead) return;

        GetComponent<Enemy_Manager>().enabled = false;
        GetComponent<Enemy_Attack>().enabled = false;
        GetComponent<Enemy_FollowPlayer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        foreach(Transform child in transform)
        {
            child.DOScale(Vector2.zero, 0.1f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                child.gameObject.SetActive(false);
            });
        }

        sparks.Play();

        float dissolve = 0;
        dissolveTween = DOTween.To(() => dissolve, x => dissolve = x, 1, dissolveTime)
            .OnUpdate(() => {
                mat = new Material(sprite.material);
                sprite.material = mat;
                mat.SetFloat("_DissolveAmount", 1 - dissolve);
            }).OnComplete(() =>
            {
                transform.DOScale(Vector3.zero, 0.1f).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
            });

        dissolveTween.Play();
        isDead = true;
    }
}
