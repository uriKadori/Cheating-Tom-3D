using GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class Student : Cell
    {
        [SerializeField] private List<Transform> availeablePlaces;
        public override CellType CellType => CellType.Student;
        public StudenData StudenData { get; private set; }
        public int Rounds { get; private set; }

        private WaitForSeconds timeToRefillRounds;
        public override void Render(CellData cellData, Vector3 pos)
        {
            base.Render(cellData, pos);
            StudenData = cellData as StudenData;
            Rounds = StudenData.CopyRounds;
            timeToRefillRounds = new WaitForSeconds(StudenData.TimeToRefillRounds);
        }

        public void Copy()
        {
            Rounds--;
            if (Rounds == 0)
            {
                StartCoroutine(RefillRounds());
            }
        }

        private IEnumerator RefillRounds()
        {
            yield return timeToRefillRounds;
            Rounds = StudenData.CopyRounds;
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
    }
}