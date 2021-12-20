using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LupiLab.LevelManagement
{
    [CreateAssetMenu(fileName = "Level Manager", menuName = "Level Management/Level Manager")]
    public class LevelManager : ScriptableObject
    {
        private const int _coreSceneIndex = 0;

        public static Scene[] ActiveScenes
        {
            get
            {
                int countLoaded = SceneManager.sceneCount;
                Scene[] loadedScenes = new Scene[countLoaded];

                for (int i = 0; i < countLoaded; i++)
                {
                    loadedScenes[i] = SceneManager.GetSceneAt(i);
                }
                return loadedScenes;
            }
        }

        public event Action FinishLoadSceneEvent;

        private Scene _uIScene;

        public void LoadSceneAdditively(int sceneIndex)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        }
        public void LoadSceneAdditively(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public void LoadScene(int sceneIndex)
        {
            UnloadAllScenes();
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
            FinishLoadSceneEvent?.Invoke();
        }
        public void LoadScene(string sceneName)
        {
            UnloadAllScenes();
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public void UnloadAllScenes()
        {
            Scene[] activeScenes = ActiveScenes;
            foreach (Scene scene in activeScenes)
            {
                if (scene.buildIndex == _coreSceneIndex) continue;
                SceneManager.UnloadSceneAsync(scene);
            }
        }

        public void UnloadScene(int sceneBuildIndex)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneBuildIndex);
        }
        public void UnloadScene(Scene scene)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(scene);
        }
        public void UnloadScene(string sceneName)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName);
        }


        





    }
}