using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_Manager pm = collision.GetComponent<Player_Manager>();
            pm.CallOnPlayerDead();
        }
    }
}
