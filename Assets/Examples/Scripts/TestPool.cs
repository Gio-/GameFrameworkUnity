using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace GameFramework.Examples {
    public class TestPool : MonoBehaviour
    {
        public GameObject object1;
        public KeyCode spawnKey1;
        public KeyCode spawnKeyNoPool1;
        public int spawnQuantity1;
        [Space]
        public GameObject object2;
        public KeyCode spawnKey2;
        public KeyCode spawnKeyNoPool2;
        public int spawnQuantity2;

        // Update is called once per frame
        void Update()
        {
            var timeAtStartFrame = Time.realtimeSinceStartup;
            //INSTANTIATE OBJECT 1 WITH POOL
            if (Input.GetKeyDown(spawnKey1))
            {
                Profiler.BeginSample("TEST_POOL_1");
                SpawnWithPool(object1, spawnQuantity1);
                Profiler.EndSample();
                Debug.Log("TEST_POOL_1 time = " + (Time.realtimeSinceStartup - timeAtStartFrame).ToString("f6"));
            }
            //INSTANTIATE OBJECT 1 WITHOUT POOL
            if (Input.GetKeyDown(spawnKeyNoPool1))
            {
                Profiler.BeginSample("TEST_INSTANTIATE_1");
                SpawnWithoutPool(object1, spawnQuantity1);
                Profiler.EndSample();
                Debug.Log("TEST_INSTANTIATE_1 time = " + (Time.realtimeSinceStartup - timeAtStartFrame).ToString("f6"));
            }

            //INSTANTIATE OBJECT 2 WITH POOL
            if (Input.GetKeyDown(spawnKey2))
            {
                Profiler.BeginSample("TEST_POOL_2");
                SpawnWithPool(object2, spawnQuantity2);
                Profiler.EndSample();
                Debug.Log("TEST_POOL_2 time = " + (Time.realtimeSinceStartup - timeAtStartFrame).ToString("f6"));
            }
            //INSTANTIATE OBJECT 1 WITHOUT POOL
            if (Input.GetKeyDown(spawnKeyNoPool2))
            {
                Profiler.BeginSample("TEST_INSTANTIATE_2");
                SpawnWithoutPool(object2, spawnQuantity2);
                Profiler.EndSample();
                Debug.Log("TEST_INSTANTIATE_2 time = " + (Time.realtimeSinceStartup - timeAtStartFrame).ToString("f6"));
            }
        }


        public void SpawnWithPool(GameObject obj, int quantity)
        {
            for (int i = 0; i <= quantity; i++)
            {
                PoolManager.GetObject(obj, true);
            }
        }

        public void SpawnWithoutPool(GameObject obj, int quantity)
        {
            for (int i = 0; i <= quantity; i++)
            {
                Instantiate(obj);
            }
        }
    }

    [System.Serializable]
    public class SpawnInfo{
        public GameObject firstObject;
        public KeyCode spawnKey;
        public KeyCode spawnKeyNoPool;
        public int spawnQuantity;
    }

}
