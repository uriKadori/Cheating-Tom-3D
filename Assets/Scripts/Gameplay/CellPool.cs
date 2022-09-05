using GameData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace GamePlay
{
    public class CellPool : MonoBehaviour
    {
        [SerializeField] private int maxPoolSize;
        [SerializeField] private List<Cell> cells;
        [SerializeField] private Player player;

        private IObjectPool<Cell> pool;
        private Cell instantiateItem;

        public Cell Get(CellType cellType)
        {
            if (pool == null)
            {
                pool = new ObjectPool<Cell>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, maxPoolSize, maxPoolSize);
            }

            if (cellType == CellType.Player)
            {
                return player;
            }

            instantiateItem = cells.Find(x => x.CellType == cellType);
            return pool.Get();
        }
        private Cell CreatePooledItem()
        {
            return Instantiate(instantiateItem, transform);
        }

        private void OnReturnedToPool(Cell system)
        {
            system.gameObject.SetActive(false);
        }

        private void OnTakeFromPool(Cell system)
        {
            system.gameObject.SetActive(true);
        }

        private void OnDestroyPoolObject(Cell system)
        {
            Destroy(system.gameObject);
        }
    }
}
