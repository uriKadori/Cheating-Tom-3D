using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameData
{

    [Serializable]
    public class CellRow
    {
        public CellData this[int index] => cells[index];
        public int Count => cells.Count;

        [SerializeField] private List<CellData> cells = new List<CellData>();
    }
}