using DG.Tweening;
using Doozy.Engine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FirePick_Behaviour : MonoBehaviour
{
    [ColorUsageAttribute(true, true)]
    [SerializeField] private Color litColor;
    [SerializeField] private Color fireColor;
    [SerializeField] private float dissolveTime = 3;
    private SpriteRenderer spriteRenderer;
    private Material mat;
    private Light2D light;
    private Light_Shake light_Shake;
    [SerializeField] private ParticleSystem spark;

    private void OnEnable()
    {
        SetInitialReferences();
    }

    private void OnDisable()
    {
        
    }

    private void SetInitialReferences()
    {
        light = GetComponentInChildren<Light2D>();
        light_Shake = light.GetComponent<Light_Shake>();
        light.color = fireColor;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = fireColor;

        mat = new Material(spriteRenderer.material);

        mat.SetColor("_DissolveOutlineColor", litColor);
        mat.SetColor("_OutlineColor", litColor);

        var main = spark.main;
        main.startColor = fireColor;

        spriteRenderer.material = mat;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_Manager pm = collision.GetComponent<Player_Manager>();
            pm.CallOnPlayerWin();

            float dissolve = 0;
            float lightRadius = light_Shake.Radius;

            Vector2 currentPos = transform.position;
            DOTween.To(() => dissolve, x => dissolve = x, 1, dissolveTime)
                .OnUpdate(() => {
                    light_Shake.Radius = Mathf.Lerp(lightRadius, 0, dissolve);
                    mat = new Material(spriteRenderer.material);
                    spriteRenderer.material = mat;
                    mat.SetFloat("_DissolveAmount", 1 - dissolve);
                    transform.position = Vector2.Lerp(currentPos, pm.transform.position, dissolve);
                }).OnComplete(() =>
                {
                    transform.DOScale(Vector3.zero, 0.1f).OnComplete(() =>
                    {
                        GameEventMessage.SendEvent("Win");
                        gameObject.SetActive(false);
                    });
                });
        }
    }
}
