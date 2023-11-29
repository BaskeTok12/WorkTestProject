using Managers.Game_Manager;
using TMPro;
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
            scoreText.text = gameManager.Scores.ToString();
        }

        private void UpdateBestScore()
        {
            Debug.Log("BestScore (from ScoreMan) :" + gameManager.BestScore);
        }

        private void ResetScore()
        {
            scoreText.text = string.Empty;
        }
    }
}