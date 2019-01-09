using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public enum SceneType
{
    GameScene,
    MenuScene,
    UiScene
}

[CreateAssetMenu(menuName = "SceneData/NewSceneData")]
public class SceneData : ScriptableObject
{
    [SerializeField]
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

    //Give the progress state on a range of(0,1)
    private float m_loadingProgress = 0;

    public float            LoadingProgress { get { return m_loadingProgress; } set { m_loadingProgress = value; } }
    public string           SceneName       { get { return m_sceneName;     }   set { m_sceneName = value; } }
    public UnityEvent       OnSceneLoaded   { get { return m_OnSceneLoaded; } }
    public LoadSceneMode    LoadType        { get { return m_loadType;      } }
    public SceneType        SceneType       { get { return m_sceneType;     } }
}
