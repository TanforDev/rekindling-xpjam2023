using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class Player_SpawnFires : MonoBehaviour
{
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private Transform grabPivot;
    [SerializeField] private KeyCode chargeKeyCode;
    [SerializeField] private bool isCharging;
    private float currentChargeTime;

    private Fire_Beahaviour currentFire;

    private Player_Manager player_Manager;
    private Player_Input player_Input;
    private Player_Health player_Health;

    [SerializeField] private float minScale = 0.5f;
    [SerializeField] private float maxScale = 1f;

    private void OnEnable()
    {
        SetInitialReferences();
    }

    private void SetInitialReferences()
    {
        player_Manager = GetComponent<Player_Manager>();
        player_Input = GetComponent<Player_Input>();
        player_Health = GetComponent<Player_Health>();
    }

    private void Update()
    {
        if (!isCharging && Input.GetMouseButtonDown(0))
        {
            if(player_Health.Health > 1)
            {
                currentFire = Instantiate(firePrefab, grabPivot).GetComponent<Fire_Beahaviour>();
                currentFire.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

                isCharging = true;
            }
            else
            {

            }
        }

        if(isCharging && currentFire != null)
        {
            float max = Mathf.Lerp(minScale, maxScale, player_Health.Health / (float)3);
            Debug.Log(max);
            if(currentChargeTime < max)
            {
                currentFire.Scale = currentChargeTime;

                currentChargeTime += Time.deltaTime;
            }
        }

        if (isCharging && Input.GetMouseButtonUp(0))
        {
            int damage = 0;
            if (currentChargeTime <= minScale)
            {
                damage = 1;
            }
            else if (currentChargeTime > minScale &&
                currentChargeTime <= (minScale + maxScale) * 0.5f)
            {
                damage = 2;
            }
            else if (currentChargeTime > (minScale + maxScale) * 0.5f)
            {
                damage = 3;
            }

            player_Manager.DamageHealth(damage);
            currentFire.StopScale(minScale, maxScale);
            currentChargeTime = 0;
            isCharging = false;

            currentFire.Release(player_Input.MouseDir, damage);
            currentFire = null;
        }
    }
}
