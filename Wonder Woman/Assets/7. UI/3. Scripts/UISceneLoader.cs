using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneLoader : MonoBehaviour
{
    private static List<string> _loadingUIScenes = new List<string>();
    private static Dictionary<string,Scene> _activeUIScenes = new Dictionary<string, Scene>();

    [SerializeField] private string _sceneName;
    [SerializeField] private bool _allowDuplicateScenes = false;

    private Scene uIScene;

    // Start is called before the first frame update
    void Start()
    {
        if (LoadScene())
        {
            Debug.Log($"{name} has started loading UI scene {_sceneName}.");
        }
        else
        {
            Debug.Log($"{name} could not start loading UI scene {_sceneName}.");
        }

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private bool LoadScene()
    {
        if (_sceneName == "")
        {
            Debug.LogWarning($"{name} has no scene name!");
            return false;
        }
        if (!_allowDuplicateScenes)
        {
            if (_loadingUIScenes.Contains(_sceneName))
            {
                Debug.LogWarning($"{name} tried to load UI scene {_sceneName}, but it is loading!");
                return false;
            }
            if (_activeUIScenes.Keys.Contains(_sceneName))
            {
                Debug.LogWarning($"{name} tried to load UI scene {_sceneName}, but it is already loaded!");
                return false;
            }
        }

        _loadingUIScenes.Add(_sceneName);
        SceneManager.LoadScene(sceneName: _sceneName, mode: LoadSceneMode.Additive);
        return true;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != _sceneName)
            return;

        if (_activeUIScenes.ContainsKey(scene.name))
            return;


        _loadingUIScenes.Remove(scene.name);
        _activeUIScenes[scene.name]= scene;
        
    }


    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 300, 300, 100));
        GUILayout.Label($"Active UI scenes: {_activeUIScenes.Keys.Count}");
        foreach(string key in _activeUIScenes.Keys)
        {
            GUILayout.Label(key);
        }
        GUILayout.EndArea();

    }
}
