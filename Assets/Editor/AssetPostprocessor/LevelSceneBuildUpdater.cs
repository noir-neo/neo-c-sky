using System.Linq;
using UnityEditor;

public class LevelSceneBuildUpdater : AssetPostprocessor
{
    private static readonly string LevelDirectoryPath = "Assets/Scenes/Level";

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        var changedAssets = importedAssets.Union(deletedAssets).Union(movedAssets).Union(movedFromAssetPaths);
        var changedAssetDirectoryNames = changedAssets.Select(System.IO.Path.GetDirectoryName);
        if (changedAssetDirectoryNames.Any(n => n == LevelDirectoryPath))
        {
            UpdateScenesInBuild();
        }
    }

    private static void UpdateScenesInBuild()
    {
        EditorBuildSettings.scenes = EditorBuildSettings.scenes
            .Where(s => !s.path.Contains(LevelDirectoryPath))
            .Concat(
                AssetDatabase.FindAssets("t:Scene")
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Where(path => path.Contains(LevelDirectoryPath))
                    .Select(path => new EditorBuildSettingsScene(path, true))
                )
                .ToArray();
    }

}


