using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    private Enemy_Manager enemy_Manager;

    [SerializeField] private float recoildDist = 2;
    [SerializeField] private float releaseDist = 12;

    [SerializeField] private float recoildTime = 2;
    [SerializeField] private float releaseTime = 0.75f;

    [SerializeField] private float attackRate = 3;
    private float attackTime;

    [SerializeField] private GameObject movementParticle;
    [SerializeField] private GameObject biteHitbox;
    [SerializeField] private GameObject normalHitbox;

    [SerializeField] private AudioClip attackClip;

    private void OnEnable()
    {
        SetInitialReferences();
        enemy_Manager.OnEnemyChargeAttack += Attack;
    }

    private void OnDisable()
    {
        transform.DOKill();
        enemy_Manager.OnEnemyDie -= Attack;
    }

    private void SetInitialReferences()
    {
        attackTime = Time.time + Random.Range(attackRate, attackRate * 1.5f);
        enemy_Manager = GetComponent<Enemy_Manager>();
        biteHitbox.SetActive(false);
        movementParticle.SetActive(false);
        normalHitbox.SetActive(true);
    }
    private void Attack()
    {
        if (Time.time < attackTime) return;

        enemy_Manager.EnemyState = EnemyState.ChargeAttack;

        Vector2 dir = enemy_Manager.Player.position - transform.position;
        dir.Normalize();

        Vector2 recoilPos = (Vector2)transform.position - dir * recoildDist;

        transform.DOMove(recoilPos, recoildTime).SetEase(Ease.OutSine).OnComplete(() =>
        {
            enemy_Manager.CallOnEnemyAttack();
            dir = enemy_Manager.Player.position - transform.position;
            dir.Normalize();

            movementParticle.SetActive(true);
            biteHitbox.SetActive(true);
            normalHitbox.SetActive(false);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            movementParticle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
            SoundMananger.Instance.PlayClip(attackClip, transform.position);
            //AudioSource.PlayClipAtPoint(attackClip, transform.position, 1);
            Vector2 finishPos = (Vector2)enemy_Manager.Player.position + dir * releaseDist;
            transform.DOMove(finishPos, releaseTime).SetEase(Ease.OutSine).OnComplete(() =>
            {
                movementParticle.SetActive(false);
                biteHitbox.SetActive(false);
                normalHitbox.SetActive(true);
                enemy_Manager.CallOnAttackEnd();
            });
        });

        attackTime = Time.time + Random.Range(attackRate, attackRate*1.5f);

    }

}
