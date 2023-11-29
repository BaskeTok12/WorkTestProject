using Common.CommonScripts.Constants;
using Managers.Game_Manager;
using TMPro;
using UnityEngine;

namespace UI.Score
{
    public class ScoreManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameManager gameManager;
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI bestScoreText;
        
        private void OnEnable()
        {
            GameManager.OnScoreIncreased += UpdateScore;

            GameManager.OnRestart += ResetScore;
            GameManager.OnBestScoreIncreased += UpdateBestScore;
        }

        private void OnDisable()
        {
            GameManager.OnScoreIncreased -= UpdateScore;

            GameManager.OnRestart -= ResetScore;
            GameManager.OnBestScoreIncreased -= UpdateBestScore;
        }

        private void UpdateScore()
        {
            var currentScore = gameManager.Scores;
            
            scoreText.text = currentScore.ToString();
        }

        private void UpdateBestScore()
        {
            var currentScore = gameManager.BestScore;
        }

        private void ResetScore()
        {
            scoreText.text = string.Empty;
        }
    }
}