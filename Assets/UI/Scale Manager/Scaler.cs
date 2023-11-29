using DG.Tweening;
using UnityEngine;

namespace UI.Scale_Manager
{
    public class Scaler : MonoBehaviour
    {
        private Vector3 _defaultScale;
        
        private void Start()
        {
            _defaultScale = transform.localScale;
        }

        private void OnEnable()
        {
            ScaleManager.OnScaleReset += ResetScale;
        }

        private void OnDisable()
        {
            ScaleManager.OnScaleReset -= ResetScale;
        }

        public void ScaleYoYo(float scaleMultiplier, float duration)
        {
            transform.DOScale(_defaultScale * scaleMultiplier, duration / 2f)
                .SetEase(Ease.OutQuad) // You can adjust the easing function as needed
                .OnComplete(() =>
                {
                    // Bounce back to the default scale
                    transform.DOScale(_defaultScale, duration / 2f)
                        .SetEase(Ease.OutBounce);
                });
        }

        public void ScaleToZero(float scaleMultiplier, float duration)
        {
            transform.DOScale(_defaultScale * scaleMultiplier, duration / 2f)
                .SetEase(Ease.OutQuad) // You can adjust the easing function as needed
                .OnComplete(() =>
                {
                    // Bounce back to the default scale
                    transform.DOScale(_defaultScale, duration / 2f)
                        .SetEase(Ease.OutBounce)
                        .OnComplete(() =>
                        {
                            // Scale down to 0
                            transform.DOScale(Vector3.zero, duration / 2f)
                                .SetEase(Ease.OutQuad);
                        });
                });
        }

        private void ResetScale()
        {
            transform.localScale = _defaultScale;
        }
    }
}