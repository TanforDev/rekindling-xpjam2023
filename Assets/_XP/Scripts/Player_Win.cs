using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class Player_Win : MonoBehaviour
{
    private Player_Manager player_manager;
    [SerializeField] private GameObject winCamera;
    private void OnEnable()
    {
        SetInitialReferences();
        player_manager.OnPlayerWin += Win;
    }

    private void OnDisable()
    {
        player_manager.OnPlayerWin -= Win;
    }

    private void SetInitialReferences()
    {
        player_manager = GetComponent<Player_Manager>();
    }

    private void Win()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.simulated = false;
        GetComponent<PlayerController>().enabled = false;
        winCamera.SetActive(true);
    }
}
