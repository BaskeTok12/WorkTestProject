using System;
using DG.Tweening;
using Main_Controller;
using Managers.Game_Manager;
using UnityEngine;

namespace UI.Scale_Manager
{
    public class ScaleManager : MonoBehaviour
    {
        public static event Action<float, float> OnObstaclesNeedToScale;
        public static event Action OnObstaclesScaled;
        public static event Action OnScaleReset;
        
        [Header("For Scaling")] 
        [SerializeField] private Scaler scoreText;
        [Header("Scale Parameters")]
        [SerializeField] private float tweenDuration;
        [SerializeField] private float scaleMultiplier;
        [Header("Sequence Parameters")]
        [SerializeField] private float sequenceDuration;

        private void OnEnable()
        {
            GameManager.OnScoreIncreased += ScaleScoreText;
            GameManager.OnRestart += ResetObjectsScale;

            PlayerBallCollisionController.OnPlayerCircleDestroyed += OnDestroyHandle;
        }

        private void OnDisable()
        {
            GameManager.OnScoreIncreased -= ScaleScoreText;
            GameManager.OnRestart -= ResetObjectsScale;
            
            PlayerBallCollisionController.OnPlayerCircleDestroyed -= OnDestroyHandle;
        }

        private void ScaleScoreText()
        {
            scoreText.ScaleYoYo(scaleMultiplier, tweenDuration);
        }

        private void ScaleObstacles()
        {
            //placedObstacles.ScaleToZero(scaleMultiplier, tweenDuration);
            OnObstaclesNeedToScale?.Invoke(scaleMultiplier, tweenDuration);
        }

        private void ResetObjectsScale()
        {
            OnScaleReset?.Invoke();
            //placedObstacles.ResetScale();
        }

        private void OnDestroyHandle()
        {
            Sequence onMissSequence = DOTween.Sequence();
            
            onMissSequence.AppendCallback(ScaleObstacles);
            
            onMissSequence.AppendInterval(sequenceDuration);
            
            onMissSequence.AppendCallback(() => OnObstaclesScaled?.Invoke());
        }
    }
}