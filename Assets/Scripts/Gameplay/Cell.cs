using GameData;
using UnityEngine;
namespace GamePlay
{
    public abstract class Cell : MonoBehaviour
    {
        public abstract CellData data { get; }
    }
}
