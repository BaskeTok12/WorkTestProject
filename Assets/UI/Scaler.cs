using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class Scaler : MonoBehaviour
    {
        private Vector3 _defaultScale;
        private void Start()
        {
            _defaultScale = transform.localScale;
        }

        public void ScaleYoYo(float scaleMultiplier, float duration)
        {
            var targetScale = transform.localScale * scaleMultiplier;

            transform.DOScale(targetScale, duration)
                .SetEase(Ease.InBounce);
            //transform.DOPunchScale(targetScale, duration);
        }

        public void ScaleToZero(float scaleMultiplier)
        {
            float duration = 0.5f;
            
            transform.DOScale(_defaultScale, duration)
                .SetEase(Ease.InBounce);
            
            /*transform.DOScaleY(_defaultScale.y * 1.3f, duration / 2)
                .SetEase(Ease.InOutBounce)
                .OnComplete(() => transform.DOScaleY(0f, duration / 2)
                    .SetEase(Ease.InOutBounce));*/
            
            
            /*transform.DOScaleY(scaleMultiplier, duration)
                .SetEase(Ease.OutBounce)
                .OnComplete(() =>
                {
                    transform.DOScaleY(0f, duration)
                        .SetEase(Ease.InBounce);
                });*/
        }

        public void ResetScale()
        {
            transform.localScale = _defaultScale;
        }
    }
}