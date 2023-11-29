using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class SpriteFader : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float fadeDuration;

        private float _defaultTransparency;

        private void Start()
        {
            _defaultTransparency = spriteRenderer.color.a;
        }

        public void FadeSpriteIn()
        {
            spriteRenderer.DOFade(_defaultTransparency, fadeDuration);
        }

        public void FadeSpriteOut()
        {
            spriteRenderer.DOFade(0f, fadeDuration);
        }
    }
}