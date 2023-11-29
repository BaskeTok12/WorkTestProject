using Managers.Game_Manager;
using TMPro;
using UI.UI_Manager;
using UnityEngine;

namespace UI.Score
{
    public class ScoreManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameManager gameManager;
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI restartPanelScoreText;
        [SerializeField] private TextMeshProUGUI isBestScoreText;
        
        private void OnEnable()
        {
            GameManager.OnScoreIncreased += UpdateScore;

            GameManager.OnRestart += ResetScore;

            UIManager.OnRestartScreenOpened += SetScoreTextOnRestartPanel;
        }

        private void OnDisable()
        {
            GameManager.OnScoreIncreased -= UpdateScore;

            GameManager.OnRestart -= ResetScore;
            
            UIManager.OnRestartScreenOpened -= SetScoreTextOnRestartPanel;
        }

        private void UpdateScore()
        {
            var currentScore = gameManager.Scores;
            
            scoreText.text = currentScore.ToString();
        }

        private void ResetScore()
        {
            scoreText.text = string.Empty;
        }

        private void SetScoreTextOnRestartPanel()
        {
            var currentScore = gameManager.Scores;
            restartPanelScoreText.text = currentScore.ToString();
            if (currentScore >= gameManager.BestScore)
            {
                isBestScoreText.gameObject.SetActive(true);
                return;
            }
            isBestScoreText.gameObject.SetActive(false);
        }
    }
}