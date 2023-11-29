using System;
using Main_Controller;
using Managers.Game_Manager;
using UI.Scale_Manager;
using UI.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UI_Manager
{
    public class UIManager : MonoBehaviour
    {
        public static event Action OnRestartScreenOpened;
        
        [Header("Managers")] 
        [SerializeField] private GameManager gameManager;
        [SerializeField] private FadingManager fadingManager;
        [Header("Panels")] 
        [SerializeField] private GameObject inGamePanel;
        [SerializeField] private GameObject restartPanel;
        [SerializeField] private Button inGamePanelButton;
        [Header("Transition")]
        [SerializeField] private ThirdPointTransition mainPanelsTransition;
        [SerializeField] private ThirdPointTransition bestScoresPanelTransition;
        [Header("Parameters")] 
        [SerializeField] private float fadeDuration;
        [Header("UI Animations")]
        [SerializeField] private Animator backgroundCircleAnimator;
        
        private void OnEnable()
        {
            GameManager.OnRestart += ShowInGamePanel;
            
            ScaleManager.OnObstaclesScaled += ShowRestartPanelFromGame;
        }

        private void OnDisable()
        {
            GameManager.OnRestart -= ShowInGamePanel;
      
            ScaleManager.OnObstaclesScaled -= ShowRestartPanelFromGame;
        }

        public void StartGame()
        {
           gameManager.StartGame();
           inGamePanelButton.enabled = false;
           //fadingManager.ShowPanel(backgroundCircle, fadeDuration);
        }
        
        public void RestartGame()
        {
            gameManager.RestartGame();
        }
        
        private void ShowInGamePanel()
        {
            mainPanelsTransition.FromFirstTransition();
            inGamePanelButton.enabled = true;
            //fadingManager.ShowPanel(backgroundCircle, fadeDuration);
        }
    
        private void ShowRestartPanelFromGame()
        {
            mainPanelsTransition.ToFirstTransition();
            //backgroundCircleAnimator.SetTrigger(Animations.FadeOutTrigger);
            OnRestartScreenOpened?.Invoke();
            //fadingManager.HidePanel(backgroundCircle, fadeDuration);
        }
        
        public void ShowRestartPanelFromHighScore()
        {
            mainPanelsTransition.FromSecondTransition();
            OnRestartScreenOpened?.Invoke();
        }
        public void ShowHighScorePanelFromRestart()
        {
            mainPanelsTransition.ToSecondTransition();
        }

        #region ForButtons
        
        public void ShowBestScoresPanel()
        {
            bestScoresPanelTransition.ToFirstTransition();
        }
        
        public void HideBestScoresPanel()
        {
            bestScoresPanelTransition.ToFirstTransition();
        }
        
        #endregion
    }
}
