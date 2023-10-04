using DG.Tweening;
using Doozy.Engine;
using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class Player_Death : MonoBehaviour
{
    private Player_Manager player_Manager;
    private PlayerController playerController;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer fireheadSprite;
    [SerializeField] private float dissolveTime = 1;

    [SerializeField] private ParticleSystem sparks;
    [SerializeField] private GameObject deathCamera;
    [SerializeField] private MMF_Player impulse;

    private Material mat;
    private Material mat2;
    private Tween dissolveTween;

    private bool isDead;

    private void OnEnable()
    {
        SetInitialReferences();
        player_Manager.OnPlayerDead += Death;
    }

    private void OnDisable()
    {
        player_Manager.OnPlayerDead -= Death;
    }

    private void SetInitialReferences()
    {
        var main = sparks.main;
        main.duration = dissolveTime - 0.6f;

        player_Manager= GetComponent<Player_Manager>();
        playerController = GetComponent<PlayerController>();
    }

    private void Death()
    {
        if (isDead) return;

        playerController.enabled = false;
        sparks.Play();
        impulse.PlayFeedbacks();

        deathCamera.SetActive(true);

        float dissolve = 0;
        dissolveTween = DOTween.To(() => dissolve, x => dissolve = x, 1, dissolveTime)
            .OnUpdate(() => {
                mat = new Material(sprite.material);
                sprite.material = mat;
                mat.SetFloat("_DissolveAmount", 1 - dissolve);

                mat2 = new Material(fireheadSprite.material);
                fireheadSprite.material = mat2;
                mat2.SetFloat("_DissolveAmount", 1 - dissolve);

            }).OnComplete(() =>
            {
                transform.DOScale(Vector3.zero, 0.1f).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
            });

        dissolveTween.Play();
        isDead = true;
        GameEventMessage.SendEvent("GameOver");
    }
}
