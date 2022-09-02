using GameData;
using UnityEngine;
using System.Collections.Generic;

namespace GamePlay
{
    public class Player : Cell
    {
        public override CellData data => throw new System.NotImplementedException();

        [SerializeField] private Transform charcater;
        public void MoveCharacter(List<Vector3Int> path)
        {

        }
    }
}