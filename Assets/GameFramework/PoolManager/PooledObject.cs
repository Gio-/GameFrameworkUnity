using UnityEngine;

namespace GameFramework
{
    public class PooledObject : MonoBehaviour
    {
        public string queueName { get; set; }

        private void OnDisable()
        {
            PoolManager.EnquequeObject(this);
        }
    }
}
