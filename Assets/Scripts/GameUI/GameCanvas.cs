using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameUi
{
    public class GameCanvas : MonoBehaviour
    {
        public event Action OnRestart;
        public event Action OnFinish;
        public event Action OnPause;

        [SerializeField] ImageFill imageFill;
        [SerializeField] Button pauseButton;
        [SerializeField] EndPopup endPopup;
        [SerializeField] Color color;

        private void Awake()
        {
            imageFill.OnFinish += OnFinish;
            endPopup.OnRestart += OnRestart;
            pauseButton.onClick.AddListener(OnPause.Invoke);
        }

        public void GrantScore(float score)
        {
            imageFill.FillImage(score, color);
        }

        public void OpenEndPopup(bool won)
        {
            pauseButton.interactable = false;
            endPopup.Render(won);
        }

        private void OnDestroy()
        {
            imageFill.OnFinish -= OnFinish;
            endPopup.OnRestart -= OnRestart;
        }
    }
}
