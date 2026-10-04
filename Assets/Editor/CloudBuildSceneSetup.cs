using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public static class CloudBuildSceneSetup
{
    [DidReloadScripts(1000)]
    private static void SetupBuildScene()
    {
        EditorApplication.delayCall += EnsureBuildScene;
    }

    private static void EnsureBuildScene()
    {
        const string scenePath = "Assets/TestScene.unity";

        if (!System.IO.File.Exists(scenePath))
        {
            Debug.LogError("CloudBuildSceneSetup: TestScene.unity was not found.");
            return;
        }

        AssetDatabase.ImportAsset(
            scenePath,
            ImportAssetOptions.ForceSynchronousImport
        );

        SceneAsset scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);

        if (scene == null)
        {
            Debug.LogError("CloudBuildSceneSetup: Could not load TestScene.unity.");
            return;
        }

        EditorBuildSettings.scenes = new[]
        {
            new EditorBuildSettingsScene(scenePath, true)
        };

        Debug.Log(
            "CloudBuildSceneSetup: TestScene.unity registered successfully for Android build."
        );
    }
}
