using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallRetryLevel : MonoBehaviour
{
    public void CallEventRetryLevel()
    {
        LevelManager.Instance.RetryLevel();
    }
}
