using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public static partial class LevelLoader
{
    public static IObservable<AsyncOperation> LoadLevelAsObservable(int level)
    {
        return LoadSceneAsObservable(levelSceneBuildIndex[level], LoadSceneMode.Additive);
    }

    public static IObservable<AsyncOperation> UnloadLevelAsObservable(int level)
    {
        return UnloadSceneAsObservable(levelSceneBuildIndex[level]);
    }

    public static IObservable<AsyncOperation> LoadSceneAsObservable(int sceneBuildIndex, LoadSceneMode mode)
    {
        return SceneManager.LoadSceneAsync(sceneBuildIndex, mode).AsObservable();
    }

    public static IObservable<AsyncOperation> UnloadSceneAsObservable(int sceneBuildIndex)
    {
        return SceneManager.UnloadSceneAsync(sceneBuildIndex).AsObservable();
    }
}


