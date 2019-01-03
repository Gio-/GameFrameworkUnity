using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public enum SceneType
{
    GameScene,
    MenuScene,
    UiScene
}

[CreateAssetMenu(menuName = "Scene/SceneData")]
public class SceneData : ScriptableObject
{
    private string m_sceneName;
    [Header("Action callled at the end of scene loaded")]
    [SerializeField]
    #pragma warning disable CS0649
    private UnityEvent m_OnSceneLoaded;
    [Header("Load Type of scene")]
    [SerializeField]
    private LoadSceneMode m_loadType = LoadSceneMode.Single;
    [SerializeField]
    private SceneType m_sceneType = SceneType.GameScene;

    public string           SceneName       { get { return m_sceneName;     } set { m_sceneName = value; } }
    public UnityEvent       OnSceneLoaded   { get { return m_OnSceneLoaded; } }
    public LoadSceneMode    LoadType        { get { return m_loadType;      } }
    public SceneType        SceneType       { get { return m_sceneType;     } }
}
