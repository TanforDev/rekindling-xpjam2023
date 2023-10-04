using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallRestartGame : MonoBehaviour
{
    public void CallEventRestartGame()
    {
        LevelManager.Instance.ResetGame();
    }
}
