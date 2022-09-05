using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "StudenData", menuName = "ScriptableObjects/StudenData", order = 1)]
    public class StudenData : CellData
    {
        public float ScoreFromStudent => scoreFromStudent;
        public float TimeToRefillRounds => timeToRefillRounds;
        public int CopyRounds => copyRounds;

        [SerializeField] float scoreFromStudent;
        [SerializeField] float timeToRefillRounds;
        [SerializeField] int copyRounds;
    }
}