using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class Player_FireTilt : MonoBehaviour
{
    [SerializeField] private float _tiltSpeed;
    [SerializeField] private float _maxTilt;

    private IPlayerController _player;

    private void OnEnable()
    {
        _player = GetComponentInParent<IPlayerController>();
    }

    private void Update()
    {
        if (_player == null) return;

        var xInput = _player.Input.x;

        HandleCharacterTilt(xInput);
    }
    private void HandleCharacterTilt(float xInput)
    {
        var runningTilt = Quaternion.Euler(0, 0, _maxTilt * xInput);
        var targetRot = runningTilt * Vector2.up;

        transform.up = Vector3.RotateTowards(transform.up, targetRot, _tiltSpeed * Time.deltaTime, 0f);
    }
}
