using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallNextLevel : MonoBehaviour
{
    public void CallEventNextLevel()
    {
        LevelManager.Instance.NextLevel();
    }
}
