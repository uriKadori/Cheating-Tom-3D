using UnityEngine;
using GameData;
using UnityEngine.AI;
using System;
using System.Collections;

namespace GamePlay
{
    public class Player : Cell, IPausable
    {
        public override CellType CellType => CellType.Player;
        public bool Cheating => transform.position != playerPos;

        public event Action OnPathComplete;

        [SerializeField] private NavMeshAgent agent;
        private bool finishPath;
        private Vector3 playerPos;

        public override void Render(Vector3 pos, float Scale)
        {
            base.Render(pos, Scale);
            agent.enabled = true;
            StartCoroutine(SetFirstPos());
        }
        public void MoveCharacter()
        {
            MoveCharacter(playerPos);
        }
        public void MoveCharacter(Vector3 target)
        {
            if (agent.isStopped)
                return;

            agent.isStopped = true;
            finishPath = false;
            agent.SetDestination(target);
            agent.isStopped = false;
        }

        public void Pause()
        {
            agent.isStopped = true;
        }
        public void UnPause()
        {
            agent.isStopped = false;
            MoveCharacter();
        }

        private void Update()
        {
            if (!finishPath && agent.hasPath && agent.remainingDistance <= agent.stoppingDistance)
            {
                finishPath = true;
                OnPathComplete?.Invoke();
            }
        }

        private IEnumerator SetFirstPos()
        {
            yield return null;
            playerPos = transform.position;
        }
    }
}