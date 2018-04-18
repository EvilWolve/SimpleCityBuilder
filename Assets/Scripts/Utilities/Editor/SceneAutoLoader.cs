using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Utilities.Editor
{
    /// <summary>
    /// Scene auto loader.
    /// </summary>
    /// <description>
    /// This class adds an Editor Tools > Scene Autoload menu containing options to select
    /// a "master scene" enable it to be auto-loaded when the user presses play
    /// in the editor. When enabled, the selected scene will be loaded on play,
    /// then the original scene will be reloaded on stop.
    ///
    /// Based on an idea on this thread:
    /// http://forum.unity3d.com/threads/157502-Executing-first-scene-in-build-settings-when-pressing-play-button-in-editor
    /// </description>
    [InitializeOnLoad]
    static class SceneAutoLoader
    {
        // Static constructor binds a playmode-changed callback.
        // [InitializeOnLoad] above makes sure this gets executed.
        static SceneAutoLoader()
        {
            EditorApplication.playModeStateChanged += SceneAutoLoader.OnPlayModeChanged;
        }

        // Menu items to select the "master" scene and control whether or not to load it.
        [MenuItem("Editor Tools/Scene Autoload/Select Master Scene...")]
        static void SelectMasterScene()
        {
            string masterScene = EditorUtility.OpenFilePanel("Select Master Scene", Application.dataPath, "unity");
            masterScene = masterScene.Replace(Application.dataPath, "Assets"); //project relative instead of absolute path
            if (!string.IsNullOrEmpty(masterScene))
            {
                SceneAutoLoader.MasterScene = masterScene;
                SceneAutoLoader.LoadMasterOnPlay = true;
            }
        }

        [MenuItem("Editor Tools/Scene Autoload/Load Master On Play", true)]
        static bool ShowLoadMasterOnPlay()
        {
            return !SceneAutoLoader.LoadMasterOnPlay;
        }

        [MenuItem("Editor Tools/Scene Autoload/Load Master On Play")]
        static void EnableLoadMasterOnPlay()
        {
            SceneAutoLoader.LoadMasterOnPlay = true;
        }

        [MenuItem("Editor Tools/Scene Autoload/Don't Load Master On Play", true)]
        static bool ShowDontLoadMasterOnPlay()
        {
            return SceneAutoLoader.LoadMasterOnPlay;
        }

        [MenuItem("Editor Tools/Scene Autoload/Don't Load Master On Play")]
        static void DisableLoadMasterOnPlay()
        {
            SceneAutoLoader.LoadMasterOnPlay = false;
        }

        // Play mode change callback handles the scene load/reload.
        static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (!SceneAutoLoader.LoadMasterOnPlay)
            {
                return;
            }

            if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)
            {
                // User pressed play -- autoload master scene.
                SceneAutoLoader.PreviousScene = SceneManager.GetActiveScene().path;
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    try
                    {
                        EditorSceneManager.OpenScene(SceneAutoLoader.MasterScene);
                    }
                    catch
                    {
                        Debug.LogError(string.Format("error: scene not found: {0}", SceneAutoLoader.MasterScene));
                        EditorApplication.isPlaying = false;
                    }
                }
                else
                {
                    // User cancelled the save operation -- cancel play as well.
                    EditorApplication.isPlaying = false;
                }
            }

            // isPlaying check required because cannot OpenScene while playing
            if (!EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
            {
                // User pressed stop -- reload previous scene.
                try
                {
                    EditorSceneManager.OpenScene(SceneAutoLoader.PreviousScene);
                }
                catch
                {
                    Debug.LogError(string.Format("error: scene not found: {0}", SceneAutoLoader.PreviousScene));
                }
            }
        }

        // Properties are remembered as editor preferences.
        const string EditorPrefLoadMasterOnPlay = "SceneAutoLoader.LoadMasterOnPlay";
        const string EditorPrefMasterScene = "SceneAutoLoader.MasterScene";
        const string EditorPrefPreviousScene = "SceneAutoLoader.PreviousScene";

        static bool LoadMasterOnPlay
        {
            get { return EditorPrefs.GetBool(SceneAutoLoader.EditorPrefLoadMasterOnPlay, false); }
            set { EditorPrefs.SetBool(SceneAutoLoader.EditorPrefLoadMasterOnPlay, value); }
        }

        static string MasterScene
        {
            get { return EditorPrefs.GetString(SceneAutoLoader.EditorPrefMasterScene, "Master.unity"); }
            set { EditorPrefs.SetString(SceneAutoLoader.EditorPrefMasterScene, value); }
        }

        static string PreviousScene
        {
            get { return EditorPrefs.GetString(SceneAutoLoader.EditorPrefPreviousScene, SceneManager.GetActiveScene().path); }
            set { EditorPrefs.SetString(SceneAutoLoader.EditorPrefPreviousScene, value); }
        }
    }
}