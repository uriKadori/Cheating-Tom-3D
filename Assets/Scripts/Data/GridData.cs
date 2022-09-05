using System.Collections.Generic;
using System;

namespace GameData
{
   [Serializable]
    public class GridData
    {
        public List<CellRow> arrays = new List<CellRow>();
        public int RowsCount => arrays.Count;
        public int ColsCount => arrays[0].Count;
        public CellData this[int i, int j]
        {
            get
            {
                if (i >= RowsCount || j >= ColsCount || i < 0 || j < 0)
                {
                    return null;
                }

                return arrays[i][j];
            }
        }
    }
}
