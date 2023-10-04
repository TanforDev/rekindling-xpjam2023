using Doozy.Engine;
using System.Collections;
using System.Collections.Generic;
using TanforGames.Core.Singletons;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int levelCount;
    public int LevelCount => levelCount;

    public void RetryLevel()
    {
        Debug.Log("RetryLevel");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void NextLevel()
    {
        levelCount++;
        if(levelCount >= 3)
        {
            GameEventMessage.SendEvent("End");
            return;
        }
        SceneManager.LoadScene("level-" + levelCount);
    }

    public void ResetGame()
    {
        levelCount = 0;
        SceneManager.LoadScene("level-" + 0);
    }
}
