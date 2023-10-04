using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private bool isFalling;
    [SerializeField] private int steps = 1;
    [SerializeField] private Transform sprite;

    private int initialSteps;
    private Vector2 initialPos;
    private Vector2 initialScale;

    private void OnEnable()
    {
        initialPos = transform.position;
        initialSteps = steps;
        initialScale = transform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            steps--;
            CheckSteps();
        }
    }

    private void CheckSteps()
    {
        if (isFalling == true) return;
        if (steps <= 0)
        {
            isFalling = true;
            sprite.DOShakePosition(1, 0.05f, 20, 0, false, false, ShakeRandomnessMode.Full).OnComplete(() =>
            {
                float endTime = 0.5f;
                transform.DOScale(Vector3.zero, endTime).SetEase(Ease.Linear);
                transform.DOMoveY(transform.position.y - 10, endTime).SetEase(Ease.Linear).OnComplete(() =>
                {
                    transform.position = initialPos;
                    steps = initialSteps;
                    transform.DOScale(initialScale, endTime).SetEase(Ease.Linear).SetDelay(5).OnComplete(() =>
                    {
                        isFalling = false;
                    });
                });
            });

        }
    }
}