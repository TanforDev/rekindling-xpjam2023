using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 mousePos;
    private Vector2 mouseDir;

    public Vector2 MouseDir => mouseDir;

    private Player_Manager player_Manager;

    private void OnEnable()
    {
        SetInitialReferences();
    }

    private void SetInitialReferences()
    {
        player_Manager = GetComponent<Player_Manager>();
        mainCamera = Camera.main;
    }
    private void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseDir = mousePos - (Vector2)transform.position;
    }
}
