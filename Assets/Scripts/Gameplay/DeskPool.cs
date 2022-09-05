using UnityEngine;
using UnityEngine.Pool;

namespace GamePlay
{
    public class DeskPool : MonoBehaviour
    {
        [SerializeField] private int maxPoolSize;
        [SerializeField] private Transform desk;

        IObjectPool<Transform> pool;

        public IObjectPool<Transform> Pool
        {
            get
            {
                if (pool == null)
                {
                    pool = new ObjectPool<Transform>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, maxPoolSize, maxPoolSize);
                }

                return pool;
            }
        }

        private Transform CreatePooledItem()
        {
            return Instantiate(desk, transform);
        }

        private void OnReturnedToPool(Transform system)
        {
            system.gameObject.SetActive(false);
        }

        private void OnTakeFromPool(Transform system)
        {
            system.gameObject.SetActive(true);
        }

        private void OnDestroyPoolObject(Transform system)
        {
            Destroy(system.gameObject);
        }
    }
}
