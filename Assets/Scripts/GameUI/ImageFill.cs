using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameUi
{
    public class ImageFill : MonoBehaviour, IPausable
    {
        private const int MAX_FILL = 1;

        public event Action OnAmountReached;
        public event Action OnFinish;

        [SerializeField] float duration;
        [SerializeField] Image image;
        [SerializeField] Vector3 growSize;
        [SerializeField] float growTime;
        [SerializeField] float shrinkTime;

        private bool filling;
        private float startAmount;
        private float endAmount;
        private float time;
        public void SetImageFill(float amount)
        {
            image.fillAmount = amount;
            startAmount = amount;
            endAmount = amount;
        }

        public void FillImage(float amount, Color color)
        {
            if (image.fillAmount >= MAX_FILL)
                return;

            image.color = color;
            time = 0;
            startAmount = image.fillAmount;
            if (image.fillAmount < endAmount)
            {
                endAmount += amount;
            }
            else
            {
                endAmount = image.fillAmount + amount;
            }

            filling = true;
        }

        public void Pause()
        {
            filling = false;
        }

        public void UnPause()
        {
            filling = true;
        }

        private void Update()
        {
            if (!filling)
            {
                return;
            }

            time += Time.deltaTime;
            image.fillAmount = Mathf.Lerp(startAmount, endAmount, time / duration);
            if (image.fillAmount >= endAmount)
            {
                Pause();
                LeanTween.scale(gameObject, growSize, growTime).setEase(LeanTweenType.easeOutQuint);
                LeanTween.scale(gameObject, Vector3.one, shrinkTime).setDelay(growTime).setEase(LeanTweenType.easeOutQuint).setOnComplete(AmountReached);
            }

            if (image.fillAmount >= 1)
            {
                Pause();
                OnFinish?.Invoke();
            }
        }
        
        private void AmountReached()
        {
            OnAmountReached?.Invoke();
        }
    }
}