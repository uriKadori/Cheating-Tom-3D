using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "StudenData", menuName = "ScriptableObjects/StudenData", order = 1)]
    public class StudenData : CellData
    {
        public float ScoreFromStudent => scoreFromStudent;
        public float TimeToRefillRounds => timeToRefillRounds;
        public int InitRounds => copyRounds;
        public int Rounds { get; private set; }

        [SerializeField] float scoreFromStudent;
        [SerializeField] float timeToRefillRounds;
        [SerializeField] int copyRounds;

        public void OnEnable()
        {
            Rounds = InitRounds;
        }

        public void Copy()
        {
            Rounds--;
            if (Rounds == 0)
            {
                RefillRounds();
            }
        }

        private async void RefillRounds()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(timeToRefillRounds), ignoreTimeScale: false);
            Rounds = InitRounds;
        }
    }
}