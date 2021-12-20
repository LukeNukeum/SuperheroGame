using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LupiLab.LevelManagement;

namespace LupiLab.Core
{
    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour
    {

        public static GameManager singleton;
        public bool IsInGame = true;

        void Awake()
        {

            DontDestroyOnLoad(gameObject);
        }

        void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("Scene Loaded: " + scene.name);
        }

        void Start()
        {
            Debug.Log("Game Manager Starting.");
        }

        void OnEnable()
        {
            if (singleton)
            {
                Debug.LogWarning("A Game Manager singleton already exists: " + singleton.name);
                Destroy(gameObject);
            }
            else
            {
                singleton = this;
                SceneManager.sceneLoaded += OnSceneFinishedLoading;
            }
        }

        void OnDisable()
        {
            if (singleton == this)
            {
                singleton = null;
                SceneManager.sceneLoaded -= OnSceneFinishedLoading;
            }
        }



        public void LoadScene(int sceneId)
        {
            Debug.Log("Load Scene " + sceneId);
            StartCoroutine(LoadAsyncScene(sceneId));

        }

        IEnumerator LoadAsyncScene(int sceneId)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneId);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}