using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class Enemy_Hitbox : MonoBehaviour
{
    [SerializeField] private float _knockbackForce = 50;
    [SerializeField] private int damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IPlayerController controller))
        {
            var dir = (Vector2)other.transform.position + other.offset - (Vector2)transform.position;
            var incomingSpeed = Vector3.Project(controller.Speed, dir); // player speed parralel to direction of explosion
            controller.ApplyVelocity(-incomingSpeed, PlayerForce.Burst); // cancel current incoming speed for more consistent bounces
            controller.SetVelocity(dir.normalized * _knockbackForce, PlayerForce.Decay);
            other.gameObject.GetComponent<Player_Manager>().HitPlayer(damage);
        }
    }
}
