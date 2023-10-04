using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FollowPlayer : MonoBehaviour
{
    private Enemy_Manager enemy_Manager;
    private Rigidbody2D rb;
    [SerializeField] private float horizontalSpeed = 4.5f;
    [SerializeField] private float verticalSpeed = 8;

    private float seedX;
    private float seedY;
    [SerializeField] private Vector2 offset;

    private void OnEnable()
    {
        SetInitialReferences();
    }
    private void SetInitialReferences()
    {
        enemy_Manager = GetComponent<Enemy_Manager>();
        rb = GetComponent<Rigidbody2D>();
        seedX = Random.Range(-99.9f, 99.9f);
        seedY = Random.Range(-99.9f, 99.9f);
    }

    private void FixedUpdate()
    {
        if(enemy_Manager.Player == null) { return; }

        Vector2 dir =((Vector2)enemy_Manager.Player.position + offset) - (Vector2)transform.position;
        Vector2 randomDir = new Vector2(
            2 * Mathf.PerlinNoise1D(seedX + Time.time) - 1, 
            2 * Mathf.PerlinNoise1D(seedY + Time.time) - 1);

        Vector2 target = (randomDir + dir.normalized);
        rb.velocity = new Vector2(target.x * horizontalSpeed, target.y * verticalSpeed);
    }


}
