using System;
using UnityEngine;

namespace GameUi
{
    public class CopyCanvas : MonoBehaviour, IPausable
    {
        public event Action OnFinishRound;
        [SerializeField] ImageFill imageFill;
        [SerializeField] Color[] colors;

        private int currRound;
        public void Render(int rounds,Vector3 pos)
        {
            currRound = rounds;
            transform.position = pos;
            imageFill.OnAmountReached += FinishCopying;
            FillImage();
        }

        public void Stop()
        {
            imageFill.OnAmountReached -= FinishCopying;
            imageFill.Pause();
            imageFill.SetImageFill(0);
        }
        public void Pause()
        {
            imageFill.Pause();
        }
        public void UnPause()
        {
            imageFill.UnPause();
        }

        private void FinishCopying()
        {
            imageFill.SetImageFill(0);
            OnFinishRound?.Invoke();
            FillImage();
        }

        private void FillImage()
        {
            if (currRound == 0)
                return;
            currRound--;
            var color = colors[currRound];
            imageFill.FillImage(1, color);
        }
    }
}