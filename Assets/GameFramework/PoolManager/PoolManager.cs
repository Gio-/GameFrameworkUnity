
using GameFramework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PoolManager
{

    public static Dictionary<string, Queue<GameObject>> pooledGroups = new Dictionary<string, Queue<GameObject>>();


    static PoolManager() // at scene change i must refresh pooledGroups
    {
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
    }

    static void OnSceneUnLoaded(Scene scene)
    {
        //TODO Sceneunloaded viene chiamato se clicco un oggetto con un materiale nella hierarchy, (BUGGG) non va bene
        PoolManager.ClearPool();
    }


    public static GameObject InstantiatePoolObject(GameObject poolObject, string prefabName = null)
    {
        GameObject poolObj;
        prefabName = prefabName == null ? poolObject.name : prefabName;
        // Instantiate the object
        poolObj = Object.Instantiate(poolObject) as GameObject;
        //Add pooledScript component , this will track onDisable and reQueue object to pool
        PooledObject pooledScript = poolObj.AddComponent<PooledObject>();
        // Object are instantiated as (clone) , so i will track the prefab name in order to enqueue object
        pooledScript.queueName = poolObject.name;
        //Disable object
        poolObj.SetActive(false);

        EnquequeObject(pooledScript);

        return poolObj;
    }

    public static void EnquequeObject(PooledObject pooledObj)
    {
        GameObject obj = pooledObj.gameObject;
        string queueName = pooledObj.queueName;
        if (!GroupExist(queueName))
        {
            //Debug.Log("NON ESISTE IL GRUPPO '"+ queueName +"'PER L' ENQUEUE e lo creo");
            pooledGroups.Add(queueName, new Queue<GameObject>());
        }
        if (!pooledGroups[queueName].Contains(obj))
        {
            pooledGroups[queueName].Enqueue(obj);
            //Debug.Log("enqueque dell oggetto");
        }
    }

    public static void AddToPool(GameObject poolObject)
    {
        InstantiatePoolObject(poolObject);
    }


    #region GET OBJECT METHODS
    public static GameObject GetObject(GameObject prefab, bool activate = true)
    {
        GameObject retObj = null;
        string searchedObj = prefab.name;

        if (GroupExist(searchedObj))
        {
            if (pooledGroups[searchedObj].Count > 0)
            {
                retObj = pooledGroups[searchedObj].Dequeue();
                //TODO STRANO CHE L' OGGETTO SIA NELLA QUEUE MA SIA ATTIVO NELLA HIERARKY, CONTROLLA BENE
                /*if (retObj.activeInHierarchy)
                    retObj = null;*/
            }
        }

        if (retObj == null)
        {
            //Debug.LogWarning("l' oggetto non è stato trovato, lo creo");
            InstantiatePoolObject(prefab);
            retObj = pooledGroups[searchedObj].Dequeue();

        }
        //if (!activate)   // if requested a non enabled object i will disable it ater instantiate
            retObj.SetActive(activate);

        return retObj;
    }


    public static GameObject GetObject(GameObject prefab, Vector3 position, bool activate = true)
    {
        GameObject obj = GetObject(prefab, false);
        obj.transform.position = position;
        if (activate)
            obj.SetActive(activate);

        return obj;
    }


    public static GameObject GetObject(GameObject prefab, Vector3 position, Transform parent, bool activate = true)
    {
        GameObject obj = GetObject(prefab, position, false);
        obj.transform.parent = parent;
        if (activate)
            obj.SetActive(activate);

        return obj;
    }

    public static GameObject GetObject(GameObject prefab, Vector3 position, Quaternion rotation, bool activate = true)
    {
        GameObject obj = GetObject(prefab, position, false);
        obj.transform.rotation = rotation;
        if (activate)
            obj.SetActive(activate);

        return obj;
    }

    public static GameObject GetObject(GameObject prefab, Vector3 position, Transform parent, Quaternion rotation, bool activate = true)
    {
        GameObject obj = GetObject(prefab, position, false);
        obj.transform.rotation = rotation;
        obj.transform.parent = parent;
        if (activate)
            obj.SetActive(activate);

        return obj;
    }
    #endregion

    public static void ClearPool()
    {
        pooledGroups.Clear();
    }

    static bool GroupExist(string groupName)
    {
        return pooledGroups.ContainsKey(groupName);
    }


}