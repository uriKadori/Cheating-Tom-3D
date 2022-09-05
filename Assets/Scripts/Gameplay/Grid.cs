using GameData;
using UnityEngine;
using System;
using Unity.AI.Navigation;

namespace GamePlay
{
    public class Grid : MonoBehaviour
    {
        public event Action<Cell> OnCellClick;
        public event Action OnCellReleased;

        [SerializeField] private NavMeshSurface surface;
        [SerializeField] private DeskPool desks;
        [SerializeField] private CellPool cells;

        public void Render(GridData gridData)
        {
            for (int i = 0; i < gridData.RowsCount; i++)
            {
                for (int j = 0; j < gridData.ColsCount; j++)
                {
                    if (gridData[i, j].CellType != CellType.Empty)
                    {
                        var cell = cells.Get(gridData[i, j].CellType);
                        cell.OnCellClick += CellClicked;
                        cell.OnCellReleased += CellReleased;
                        cell.gameObject.SetActive(true);
                        cell.Render(new Vector3(i, 0, j), gridData[i, j].Scale);

                        if (gridData[i, j].NeedDesk)
                        {
                            var desk = desks.Pool.Get();
                            desk.position = cell.transform.position + Vector3.left;
                            desk.gameObject.SetActive(true);
                        }
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