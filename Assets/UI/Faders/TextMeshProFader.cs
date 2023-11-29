using DG.Tweening;
using TMPro;
using UI.UI_Manager;
using UnityEngine;

namespace UI.Faders
{
    public class TextMeshProFader : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        [SerializeField] private float fadeDuration;
        [SerializeField] private float delayBetweenFades;

        private Sequence _sequence;

        private void OnEnable()
        {
            UIManager.OnRestartScreenOpened += StartFadingLoop;
            UIManager.OnGameScreenOpened += StopFadingLoop;
        }

        private void OnDisable()
        {
            UIManager.OnRestartScreenOpened -= StartFadingLoop;
            UIManager.OnGameScreenOpened -= StopFadingLoop;
        }

        private void StartFadingLoop()
        {
            _sequence = DOTween.Sequence();
            
            _sequence.Append(textMeshPro.DOFade(0f, fadeDuration));
            
            _sequence.AppendInterval(delayBetweenFades);
            
            _sequence.Append(textMeshPro.DOFade(1f, fadeDuration));
            
            _sequence.AppendInterval(delayBetweenFades);

            _sequence.SetLoops(-1);
        }

        private void StopFadingLoop()
        {
            if (_sequence == null || !_sequence.IsActive()) return;
            
            _sequence.Kill();
            _sequence = null;
        }
    }
}
