using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneEventSystem
{
    public static class SceneLoaderHelper
    {
        public static void LoadScene(in SceneAsset sceneAsset, in LoadSceneMode sceneMode = LoadSceneMode.Single)
        {
            if (sceneAsset == null)
            {
                Debug.LogError("SceneAsset is null.");
                return;
            }
            
            var scenePath = AssetDatabase.GetAssetPath(sceneAsset);
            SceneManager.LoadScene(scenePath, sceneMode);
        }
    
        public static void LoadSceneAsync(in SceneAsset sceneAsset, in LoadSceneMode sceneMode = LoadSceneMode.Additive)
        {
            if (sceneAsset == null)
            {
                Debug.LogError("SceneAsset is null.");
                return;
            }
            
            var scenePath = AssetDatabase.GetAssetPath(sceneAsset);
            SceneManager.LoadSceneAsync(scenePath,sceneMode);
        }
        
        public static void UnloadSceneAsync(in SceneAsset sceneAsset, in UnloadSceneOptions sceneMode = UnloadSceneOptions.None)
        {
            if (sceneAsset == null)
            {
                Debug.LogError("SceneAsset is null.");
                return;
            }

            var scenePath = AssetDatabase.GetAssetPath(sceneAsset);
            SceneManager.UnloadSceneAsync(scenePath, sceneMode);
        }
    }
}
