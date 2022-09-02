using UnityEngine;

namespace GameData
{
    public class CellData
    {
        public virtual CellType CellType => CellType.Empty;
        public Vector3Int Position { get; set; }
    }

    public enum CellType
    {
        Empty,
        Student,
        Player,
        Bolcker
    }
}