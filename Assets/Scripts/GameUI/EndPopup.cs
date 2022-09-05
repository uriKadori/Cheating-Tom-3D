using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameUi
{
    public class EndPopup : MonoBehaviour
    {
        public event Action OnRestart;

        [SerializeField] string wonText;
        [SerializeField] string looseText;
        [SerializeField] TextMeshProUGUI message;
        [SerializeField] Button restartButton;
        [SerializeField] Vector3 scaleAnim;
        [SerializeField] float scaleTime;

        private void Awake()
        {
            restartButton.onClick.AddListener(OnRestart.Invoke);
        }
        public void Render(bool won)
        {
            gameObject.SetActive(true);
            LeanTween.scale(gameObject, scaleAnim, scaleTime).setEase(LeanTweenType.easeOutElastic);
            if (won)
            {
                message.text = wonText;
            }
            else
            {
                message.text = looseText;
            }
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveListener(OnRestart.Invoke);
        }
    }
}