using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    #region Private Field
    ///Scene Controller Instance
    private static SceneController instance;
    #endregion
 
    /// Constructor
    public static SceneController Instance {
        get {
            if (instance != null)
                return instance;
            instance = FindObjectOfType<SceneController>();
            if (instance != null)
                return instance;
            Create();
            return instance;
        }
    }
    public static SceneController Create()
    {
        GameObject sceneControllerGameObject = new GameObject("SceneController");
        instance = sceneControllerGameObject.AddComponent<SceneController>();
        return instance;
    }

    #region Unity-Method
    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Public-Method
    /// <summary>
    /// Call Load Scene
    /// </summary>
    /// <param name="_targetScene">scene data container</param>
    public void TransitionToScene(SceneData _targetScene)
    {
        Instance.StartCoroutine(Transition(_targetScene));
    }

    public void UnloadAsync(SceneData _sceneData)
    {
        Instance.StartCoroutine(Unloading(_sceneData));
    }

    /// <summary>
    /// Remove scene 
    /// </summary>
    /// <param name="_sceneData"></param>
    public void RemoveScene(SceneData _sceneData)
    {
        SceneManager.UnloadScene(_sceneData.SceneName);
    }
    
    /// <summary>
    /// Add additional scene Additive
    /// </summary>
    /// <param name="_sceneData"></param>
    public void AddScene(SceneData _sceneData)
    {
        SceneManager.LoadScene(_sceneData.SceneName, LoadSceneMode.Additive);
    }

    #endregion

    #region Private-Method
    /// <summary>
    /// Called to start the loading async of scene
    /// </summary>
    /// <param name="_sceneData"></param>
    /// <returns></returns>
    protected IEnumerator Transition(SceneData _sceneData)
    {
        _sceneData.LoadingProgress = 0;

        AsyncOperation _asyncOperation = SceneManager.LoadSceneAsync(_sceneData.SceneName, _sceneData.LoadType);

        while(_asyncOperation.isDone && _asyncOperation.progress < .9f)
        {
            _sceneData.LoadingProgress = _asyncOperation.progress;
            yield return null;
        }
        _sceneData.LoadingProgress = _asyncOperation.progress;
        ///Call Load scene action
        _sceneData.OnSceneLoaded?.Invoke();
    }

    /// <summary>
    /// Called to start the loading async of scene
    /// </summary>
    /// <param name="_sceneData"></param>
    /// <returns></returns>
    protected IEnumerator Unloading(SceneData _sceneData)
    {
        ///Start load Async
        yield return SceneManager.UnloadSceneAsync(_sceneData.SceneName);
    }
    #endregion
}
