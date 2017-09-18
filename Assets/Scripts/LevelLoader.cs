using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public static partial class LevelLoader
{
    private static ISubject<int> loadLevelSubject;
    private static int currentLevel;

    public static ISubject<int> LoadLevel(int level)
    {
        if (loadLevelSubject == null)
        {
            loadLevelSubject = new Subject<int>();
            loadLevelSubject
                .SelectMany(x => LoadLevelAsObservable(x)
                    .Select(_ => x))
                .Pairwise()
                .SelectMany(x => UnloadLevelAsObservable(x.Previous))
                .Subscribe();
        }

        loadLevelSubject.OnNext(level);
        currentLevel = level;
        return loadLevelSubject;
    }

    public static ISubject<int> ReloadLevel()
    {
        return LoadLevel(currentLevel);
    }

    public static ISubject<int> LoadNextLevel()
    {
        return LoadLevel(currentLevel + 1);
    }

    public static bool ExistsNextLevel()
    {
        return levelSceneBuildIndex.ContainsKey(currentLevel + 1);
    }

    public static IObservable<AsyncOperation> LoadLevelAsObservable(int level)
    {
        return LoadSceneAsObservable(levelSceneBuildIndex[level], LoadSceneMode.Additive);
    }

    public static IObservable<AsyncOperation> UnloadLevelAsObservable(int level)
    {
        return UnloadSceneAsObservable(levelSceneBuildIndex[level]);
    }

    private static IObservable<AsyncOperation> LoadSceneAsObservable(int sceneBuildIndex, LoadSceneMode mode)
    {
        return SceneManager.LoadSceneAsync(sceneBuildIndex, mode).AsObservable();
    }

    private static IObservable<AsyncOperation> UnloadSceneAsObservable(int sceneBuildIndex)
    {
        return SceneManager.UnloadSceneAsync(sceneBuildIndex).AsObservable();
    }
}