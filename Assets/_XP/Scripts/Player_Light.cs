using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player_Light : Light_Shake
{
    [SerializeField] private float lightMultiplayer = 5;
    private Player_Health player_Health;

    private float currentRatio = 0;

    private Tween lightTween;

    [SerializeField] private Transform head;
    [SerializeField] private float maxSize = 1;
    [SerializeField] private float minSize = 0.25f;

    private void OnEnable()
    {
        SetInitialReferences();
        player_Health.OnHealthUpdated += UpdateLightRadius;
    }

    private void OnDisable()
    {
        player_Health.OnHealthUpdated -= UpdateLightRadius;
    }

    protected override void SetInitialReferences()
    {
        base.SetInitialReferences();
        player_Health = FindObjectOfType<Player_Health>();
        currentRatio = 1;
    }

    private void UpdateLightRadius(int currentHealth)
    {
        float newRatio = (float)currentHealth / (float)player_Health.MaxHealth;
        newRatio = Mathf.Clamp(newRatio, 0, 1);


        if(lightTween != null)
        {
            lightTween.Kill();
            lightTween = null;
        }
        float time = 0;
        float tempRatio = currentRatio;
        lightTween = DOTween.To(() => time, x => time = x, 1, 1).SetEase(Ease.OutSine)
            .OnUpdate(() =>
            {
                float value = Mathf.Lerp(tempRatio, newRatio, time);
                Radius = value * lightMultiplayer;
                head.localScale = Vector3.one * Mathf.Lerp(minSize, maxSize, value);
            });
        lightTween.Play();

        currentRatio = newRatio;
    }
}
