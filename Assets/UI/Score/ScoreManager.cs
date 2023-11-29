using Managers.Game_Manager;
using TMPro;
using UI.Score;
using UnityEngine;

namespace UI.ScoreManager
{
    public class ScoreManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameManager gameManager;
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI bestScoreText;

        private readonly string _highScore = "HighScore";
        
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
            
            PlayerPrefs.SetInt(_highScore, currentScore);
            HighScores.UploadScore(gameManager.GetCurrentPlayerName(), currentScore);
        }

        private void ResetScore()
        {
            scoreText.text = string.Empty;
        }
    }
}