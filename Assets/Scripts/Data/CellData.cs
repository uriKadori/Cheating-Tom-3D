using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "CellData", menuName = "ScriptableObjects/CellData", order = 1)]
    public class CellData : ScriptableObject
    {
        public CellType CellType => cellType;
        public bool NeedDesk => needDesk;
        public float Scale => scale;

        [SerializeField] private CellType cellType;
        [SerializeField] private bool needDesk;
        [SerializeField] private float scale;
    }
}