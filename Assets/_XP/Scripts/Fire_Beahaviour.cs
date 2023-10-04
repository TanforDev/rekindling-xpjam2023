using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Beahaviour : MonoBehaviour
{
    [SerializeField] private Light_Shake light_Shake;
    [SerializeField] private float lightMultiplier = 5;
    private Rigidbody2D rb;

    private float lifeTime;
    [SerializeField] private float minLifeTime = 10;
    [SerializeField] private float maxLifeTime = 20;

    [SerializeField] private float launchForce = 100;

    private float scale;
    public float Scale
    {
        set
        {
            scale = value;
            UpdateScale();
        }
    }

    private bool isOnEnemy;
    private Enemy_Manager enemy;
    [SerializeField] private float damageRate = 0.5f;
    private float damageTime;

    [SerializeField] private int damage;

    [SerializeField] private AudioClip shoot;

    private void OnEnable()
    {
        SetInitialReferences();
    }

    private void OnDisable()
    {
        
    }

    private void SetInitialReferences()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.simulated = false;
        transform.localScale = Vector3.zero;
        //transform.DOScale(initalSize, 0.1f).SetEase(Ease.OutSine);
    }

    public void StopScale(float minScale, float maxScale)
    {
        float newScale = scale;
        if (scale <= minScale)
        {
            newScale = minScale;
            lifeTime = minLifeTime;
        }
        else if (scale > minScale && scale <= (minScale + maxScale) * 0.5f)
        {
            newScale = (minScale + maxScale) * 0.5f;
            lifeTime = (minLifeTime + maxLifeTime) * 0.5F;
        }
        else if (scale > (minScale + maxScale) * 0.5f && scale <= maxScale)
        {
            newScale = maxScale;
            lifeTime = maxLifeTime;
        }

        scale = newScale;
        transform.DOScale(Vector3.one * newScale, 0.1f).SetEase(Ease.OutSine).OnUpdate(() =>
        {
            UpdateLightScale(transform.localScale.magnitude);
        });
    }

    private void UpdateScale()
    {
        transform.localScale = Vector3.one * scale;
        UpdateLightScale(transform.localScale.magnitude);
    }

    public void Release(Vector2 dir, int damage)
    {
        SoundMananger.Instance.PlayClip(shoot, transform.position);
        this.damage = damage;
        transform.parent = null;
        rb.isKinematic = false;
        rb.simulated = true;

        if(dir != Vector2.zero)
        {
            rb.AddForce(dir.normalized * launchForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            return;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy = collision.gameObject.GetComponent<Enemy_Manager>();
            enemy.DamageEnemy(damage);
            damageTime = Time.time + damageRate;
            isOnEnemy = true;
        }


        transform.SetParent(collision.transform, true);
        transform.DOScale(Vector3.zero, minLifeTime).SetEase(Ease.OutSine).OnUpdate(() =>
        {
            UpdateLightScale(transform.localScale.magnitude);
        }).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });

        rb.isKinematic = true;
        rb.simulated = false;
    }

    private void UpdateLightScale(float value)
    {
        light_Shake.Radius = value * lightMultiplier;
    }

    private void Update()
    {
        if (isOnEnemy)
        {
            if(damageTime < Time.time)
            {
                enemy.DamageEnemy(1);
                damageTime = Time.time + damageRate;
            }
        }
    }
}
