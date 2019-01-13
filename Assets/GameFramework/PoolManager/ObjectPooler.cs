namespace GameFramework
{
    using System.Collections.Generic;
    using UnityEngine;

    public class ObjectPooler : MonoBehaviour {
        [System.Serializable]
        public class PoolObject
        {
            public GameObject objectToPool;
            public int poolQuantity;
        }
        
        public List<PoolObject> pool;


        private void OnEnable()
        {
            foreach(PoolObject poolObj in pool) { 
                for (int i = 0; i < poolObj.poolQuantity; i++)
                {
                    PoolManager.AddToPool(poolObj.objectToPool);
                }
            }
        }


    }
}