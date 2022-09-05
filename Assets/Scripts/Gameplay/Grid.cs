using GameData;
using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.AI.Navigation;

namespace GamePlay
{
    public class Grid : MonoBehaviour
    {
        public event Action<Cell> OnCellClick;
        public event Action OnCellReleased;
        public int SpaceSize => spaceSize;

        [SerializeField] private List<Cell> cells;
        [SerializeField] private int spaceSize = 5;
        [SerializeField] private NavMeshSurface surface;
        [SerializeField] private List<Transform> desks;

        public void Render(GridData gridData)
        {
            int k = 0;
            for (int i = 0; i < gridData.RowsCount; i++)
            {
                for (int j = 0; j < gridData.ColsCount; j++)
                {
                    if (gridData[i, j].CellType != CellType.Empty)
                    {
                        var cell = cells.Find(x => !x.gameObject.activeSelf && x.CellType == gridData[i, j].CellType);
                        cell.OnCellClick += CellClicked;
                        cell.OnCellReleased += CellReleased;
                        cell.gameObject.SetActive(true);
                        var pos = new Vector3Int(i, 0, j) * SpaceSize;
                        if (gridData[i, j].NeedDesk)
                        {
                            desks[k].position = pos + Vector3.left;
                            desks[k].gameObject.SetActive(true);
                            k++;
                        }

                        cell.Render(gridData[i, j], pos);
                    }
                }
            }

            surface.BuildNavMesh();
        }

        private void CellClicked(Cell cellPos)
        {
            OnCellClick?.Invoke(cellPos);
        }
        private void CellReleased()
        {
            OnCellReleased?.Invoke();
        }
    }
}