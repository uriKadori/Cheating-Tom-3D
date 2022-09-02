using GameData;
using UnityEngine;
using System.Collections.Generic;

namespace GamePlay
{
    public class Grid : MonoBehaviour
    {
        public int SpaceSize => spaceSize;

        [SerializeField] private List<Cell> cells;
        [SerializeField] private int spaceSize = 5;
        public void Render(GridData gridData)
        {
            for (int i = 0; i < gridData.RowsCount; i++)
            {
                for (int j = 0; j < gridData.ColsCount; j++)
                {
                    if (gridData[i, j].CellType != CellType.Empty)
                    {
                        var cell = cells.Find(x => !x.gameObject.activeSelf && x.data.CellType == gridData[i, j].CellType);
                        cell.transform.position = gridData[i, j].Position * SpaceSize;
                        cell.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}