using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Lobby = 0,
    Game = 1,
} 
public class GameManager : MonoSingleton<GameManager>
{
    public PoolList poolList;
    private void Awake() {
        PoolManager.Instance.Init(poolList);
    }
    public void SceneChange(SceneType sceneType, Action EndChangeScene = null) 
    {
        StartCoroutine(SceneChangeCor(sceneType, EndChangeScene));
    }
    private IEnumerator SceneChangeCor(SceneType sceneType, Action EndChangeScene)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneBuildIndex:(int)sceneType);
        while(!asyncOperation.isDone)
        {
            yield return null;
        }
        EndChangeScene?.Invoke();
    }
}
