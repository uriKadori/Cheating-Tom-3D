using GameData;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class Student : Cell
    {
        [SerializeField] private List<Transform> availeablePlaces;
        public override CellType CellType => CellType.Student;
        public override void Render(Vector3 pos, float Scale)
        {
            base.Render(pos, Scale);
        }

        public Vector3 GetNearestPlaceToCell(Vector3 position)
        {
            var min = float.MaxValue;
            var nearestPlace = transform.position;
            foreach (var place in availeablePlaces)
            {
                var dist = Vector3.Distance(place.position, position);
                if (min > dist)
                {
                    min = dist;
                    nearestPlace = place.position;
                }
            }

            return nearestPlace;
        }

        public static implicit operator Student(CellData v)
        {
            throw new System.NotImplementedException();
        }
    }
}