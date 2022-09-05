using System;
using System.Collections;
using UnityEngine;

namespace GamePlay
{
    public class Teacher : MonoBehaviour, IPausable
    {
        public event Action OnHit;

        [SerializeField] Vision vision;
        private WaitForSeconds waitLook;

        private void Awake()
        {
            vision.OnHit += OnHit;
        }
        public void Render(float teacherTimeToLook)
        {
            waitLook = new WaitForSeconds(teacherTimeToLook);
            StartCoroutine(Watching());
        }
        public void Pause()
        {
            vision.Pause();
        }

        public void UnPause()
        {
            vision.UnPause();
        }

        private IEnumerator Watching()
        {
            while (true)
            {
                yield return waitLook;
                yield return vision.Render();
            }
        }
        private void OnDestroy()
        {
            vision.OnHit -= OnHit;
        }
    }
}