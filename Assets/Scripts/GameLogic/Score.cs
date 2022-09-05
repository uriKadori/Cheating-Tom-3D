using UnityEngine;
namespace GameLogic
{
    public class Score
    {
        private const int MAX_SCORE = 1;
        [SerializeField] float amount;

        public Score()
        {
            amount = 0;
        }

        internal bool GrantScore(float scoreFromStudent)
        {
            amount += scoreFromStudent;
            return amount >= MAX_SCORE;
        }
    }
}