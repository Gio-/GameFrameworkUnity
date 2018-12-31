/*
 *  Gio- 
 * 
 *  Known Issue: Unity v.2017.2 has some bug with sceneUnloaded event. When 
 *  you click IN EDITOR some objects with material component attached, it may 
 *  be throw the sceneUnloaded event without reason. By the way, this is only
 *  in Editor and it will not happen in build.
 */

/*#*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManagerHelper : MonoBehaviour
{
    /// Make this object persistent between scenes
    /// in order to save up some memory when Event 
    /// Manager is called. Add a subscription to 
    /// sceneUnloaded event.
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        #if !UNITY_EDITOR
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        #endif
    }
    
    /// When disable, unsubscribe from sceneUnloaded
    /// event. This line will prevent memory leak problems.
    private void OnDisable()
    {
        #if !UNITY_EDITOR
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        #endif
    }

    /// When this object is destroyed, then clean
    /// everything from EventManager.
    private void OnDestroy()
    {
        EventManager.Clean();
    }

    /// <summary>
    /// Function called at sceneUnloaded event: clean 
    /// every event registered in EventManager.
    /// </summary>
    /// <param name="scene">Scene unloaded.</param>
    #if !UNITY_EDITOR
    public void OnSceneUnloaded(Scene scene)
    {
        EventManager.Clean();
    }
    #endif
}
