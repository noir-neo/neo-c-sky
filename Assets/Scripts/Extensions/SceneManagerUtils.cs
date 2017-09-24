using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManagerUtils
{
    public static IEnumerable<Scene> LoadedScenes()
    {
        return Enumerable.Range(0, SceneManager.sceneCount).Select(SceneManager.GetSceneAt);
    }

    public static IEnumerable<GameObject> RootGameObjects()
    {
        return LoadedScenes().SelectMany(s => s.GetRootGameObjects());
    }

    public static T GetComponentInRoot<T>() where T : Component => RootGameObjects().Select(x => x.GetComponent<T>()).FirstOrDefault(x => x != null);
    public static IEnumerable<T> GetComponentsInRoot<T>() where T : Component => RootGameObjects().Select(x => x.GetComponent<T>()).Where(x => x != null);
}