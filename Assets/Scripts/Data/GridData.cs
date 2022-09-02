using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    public class GridData
    {
        public int RowsCount => cells.GetLength(0);
        public int ColsCount => cells.GetLength(1);
        public GridData()
        {
            //TODO change to be nice from editor
            cells = new CellData[,]
            {
            { new StudentData(),new CellData(),new CellData(),new StudentData(),new CellData(),new CellData(),new PlayerData()},
            { new StudentData(),new CellData(),new CellData(),new StudentData(),new CellData(),new CellData(),new StudentData()},
            { new StudentData(),new CellData(),new CellData(),new StudentData(),new CellData(),new CellData(),new StudentData()}
            };

            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColsCount; j++)
                {
                    this[i, j].Position = new Vector3Int(i, 0, j);
                }
            }
        }

        /// <summary>
        /// kind of a* pathfinding
        /// </summary>
        public List<Vector3Int> GetPath(CellData origin, CellData target)
        {
            var path = new List<Vector3Int>() { origin.Position };
            var sawPos = new HashSet<Vector3Int>();
            while (!path[path.Count - 1].Equals(target.Position))
            {
                var choosenPos = path[path.Count - 1];
                var minTotal = int.MaxValue;
                var minDistanceFromB = int.MaxValue;
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (i == 0 && j == 0)
                        {
                            continue;
                        }

                        var cell = this[i + path[path.Count - 1].x, j + path[path.Count - 1].z];
                        if (cell == null || sawPos.Contains(cell.Position) || cell.CellType != CellType.Empty)
                        {
                            continue;
                        }

                        sawPos.Add(cell.Position);
                        var aPos = origin.Position - cell.Position;
                        var distanceFromA = Mathf.Abs(aPos.x) + Mathf.Abs(aPos.z);
                        var bPos = target.Position - cell.Position;
                        var distanceFromB = Mathf.Abs(bPos.x) + Mathf.Abs(bPos.z);
                        var total = distanceFromA + distanceFromB;
                        if (total <= minTotal && distanceFromB < minDistanceFromB)
                        {
                            minTotal = total;
                            minDistanceFromB = distanceFromB;
                            choosenPos = cell.Position;
                        }
                    }
                }

                path.Add(choosenPos);
                if (choosenPos.Equals(target.Position))
                {
                    break;
                }
            }

            return path;
        }

        private CellData[,] cells;

        public CellData this[int i, int j]
        {
            get
            {
                if (i >= RowsCount || j >= ColsCount || i < 0 || j < 0)
                {
                    return null;
                }

                return cells[i, j];
            }
        }
    }
}
