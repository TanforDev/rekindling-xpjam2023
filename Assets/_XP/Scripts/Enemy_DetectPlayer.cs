using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class Enemy_DetectPlayer : MonoBehaviour
{
    private Enemy_Manager enemy_Manager;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float attackRadius;

    [SerializeField] LayerMask playerMask;
    [SerializeField] LayerMask levelMask;

    private void OnEnable()
    {
        SetInitialReferences();
    }
    private void SetInitialReferences()
    {
        enemy_Manager = GetComponent<Enemy_Manager>();
    }

    void Update()
    {
        if (enemy_Manager.Player != null)
        {
            float dist = Vector2.Distance(transform.position, enemy_Manager.Player.position);
            if (dist < attackRadius)
            {
                enemy_Manager.CallOnEnemyChargeAttack();
            }

            return;
        }

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerMask);
        foreach (var col in cols)
        {
            if (enemy_Manager.Player != null)
            {
                return;
            }
            if (col.gameObject.CompareTag("Player"))
            {
                RaycastHit2D hit = Physics2D.Linecast(transform.position, col.transform.position, levelMask);

                if (hit.collider == null)
                {
                    // No obstacles between the enemy and the player, so detect the player.
                    enemy_Manager.CallDetectPlayer(col.transform);
                }
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.blue;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, detectionRadius);

        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, attackRadius);
    }
#endif
}
