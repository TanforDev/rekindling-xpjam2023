using Doozy.Engine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToContinue : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameEventMessage.SendEvent("Continue");
        }
    }
}
