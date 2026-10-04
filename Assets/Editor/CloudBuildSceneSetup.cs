using UnityEditor;
using UnityEditor.Build.Profile;
using UnityEngine;

[InitializeOnLoad]
public static class CloudBuildSceneSetup
{
    static CloudBuildSceneSetup()
    {
        EnsureBuildScene();
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

        string guid = AssetDatabase.AssetPathToGUID(scenePath);

        if (string.IsNullOrEmpty(guid))
        {
            Debug.LogError("CloudBuildSceneSetup: TestScene.unity has no valid GUID.");
            return;
        }

        EditorBuildSettingsScene[] scenes =
        {
            new EditorBuildSettingsScene(scenePath, true)
        };

        // Set the global scene list.
        EditorBuildSettings.globalScenes = scenes;

        // Also set the active Build Profile scene list when one exists.
        BuildProfile activeProfile = BuildProfile.GetActiveBuildProfile();

        if (activeProfile != null)
        {
            activeProfile.scenes = scenes;
            activeProfile.overrideGlobalScenes = true;

            EditorUtility.SetDirty(activeProfile);
            AssetDatabase.SaveAssets();

            Debug.Log(
                "CloudBuildSceneSetup: TestScene.unity registered in ACTIVE BUILD PROFILE."
            );
        }
        else
        {
            Debug.Log(
                "CloudBuildSceneSetup: TestScene.unity registered in GLOBAL BUILD SETTINGS."
            );
        }

        // Final verification.
        EditorBuildSettingsScene[] finalScenes = EditorBuildSettings.scenes;

        if (finalScenes != null && finalScenes.Length > 0)
        {
            Debug.Log(
                "CloudBuildSceneSetup: VERIFIED scene count = "
                + finalScenes.Length
                + ", first scene = "
                + finalScenes[0].path
            );
        }
        else
        {
            Debug.LogError(
                "CloudBuildSceneSetup: VERIFICATION FAILED - scene list is empty."
            );
        }
    }
}
