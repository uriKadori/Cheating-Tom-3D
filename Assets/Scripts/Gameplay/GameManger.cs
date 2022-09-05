using GameData;
using GameUi;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace GamePlay
{
    public class GameManger : MonoBehaviour
    {
        [SerializeField] private LevelData level;
        [SerializeField] private Grid grid;
        [SerializeField] private Player player;
        [SerializeField] private Camera cam;
        [SerializeField] private PhysicsRaycaster physicsRaycaster;
        [SerializeField] private CopyCanvas copyCanvas;
        [SerializeField] private GameCanvas gameCanvas;
        [SerializeField] private Teacher teacher;

        private List<IPausable> pausables;
        private Score score;
        private Student target;
        private bool copying;
        private bool isPause;

        private void Awake()
        {
            grid.Render(level.GridData);
            grid.OnCellClick += CellClick;

            copyCanvas.OnFinishRound += FinishCopying;

            teacher.OnHit += CaughtCheating;
            teacher.Render(level.TeacherTimeToLook);

            gameCanvas.OnPause += PauseGame;
            gameCanvas.OnRestart += Restart;
            gameCanvas.OnFinish += WonGame;

            score = new Score();

            pausables = new List<IPausable>();
            pausables.Add(player);
            pausables.Add(teacher);
            pausables.Add(copyCanvas);
        }

        private void CellClick(Cell target)
        {
            if (target is Student student)
            {
                this.target = student;
                grid.OnCellReleased += StudentReleased;
                Vector3 nearestPlace = this.target.GetNearestPlaceToCell(player.transform.position);
                player.OnPathComplete += CopyFromStudent;
                player.MoveCharacter(nearestPlace);
            }
        }

        private void StudentReleased()
        {
            target = null;
            grid.OnCellReleased -= StudentReleased;
            player.OnPathComplete -= CopyFromStudent;
            player.MoveCharacter();
            if (copying)
            {
                copyCanvas.Stop();
            }

            copying = false;
        }

        private void CopyFromStudent()
        {
            copying = true;
            Vector3 nearestPlace = target.GetNearestPlaceToCell(player.transform.position);
            var student = (StudenData)level.GridData[target.Col, target.Row];
            if (student.Rounds > 0)
            {
                copyCanvas.Render(student.Rounds, nearestPlace);
            }
        }

        private void FinishCopying()
        {
            if (target == null)
            {
                return;
            }

            var student = (StudenData)level.GridData[target.Col, target.Row];
            if (student.Rounds == 0)
            {
                return;
            }

            student.Copy();
            var scoreFromStudent = student.ScoreFromStudent;
            score.GrantScore(scoreFromStudent);
            gameCanvas.GrantScore(scoreFromStudent);
        }

        private void WonGame()
        {
            PauseGame();
            gameCanvas.OpenEndPopup(true);
        }

        private void CaughtCheating()
        {
            if (player.Cheating)
            {
                PauseGame();
                gameCanvas.OpenEndPopup(false);
            }
        }
        private void PauseGame()
        {
            if (isPause)
            {
                physicsRaycaster.enabled = true;
                foreach (var pausable in pausables)
                {
                    pausable.UnPause();
                }
            }
            else
            {
                physicsRaycaster.enabled = false;
                foreach (var pausable in pausables)
                {
                    pausable.Pause();
                }
            }

            isPause = !isPause;

        }
        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnDestroy()
        {
            grid.OnCellClick -= CellClick;
            grid.OnCellReleased -= StudentReleased;
            copyCanvas.OnFinishRound -= FinishCopying;
            teacher.OnHit -= CaughtCheating;
            gameCanvas.OnPause -= PauseGame;
            gameCanvas.OnFinish -= WonGame;
            gameCanvas.OnRestart -= Restart;
        }
    }
}