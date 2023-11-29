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
        [SerializeField] private TwoPointTransition mainPanelsTransition;
        [SerializeField] private TwoPointTransition bestScoresPanelTransition;
        [Header("Parameters")] 
        [SerializeField] private float fadeDuration;
        [Header("UI Animations")]
        [SerializeField] private Animator backgroundCircleAnimator;
        
        private void OnEnable()
        {
            GameManager.OnRestart += ShowInGamePanel;
            
            ScaleManager.OnObstaclesScaled += ShowRestartPanel;
        }

        private void OnDisable()
        {
            GameManager.OnRestart -= ShowInGamePanel;
      
            ScaleManager.OnObstaclesScaled -= ShowRestartPanel;
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
            mainPanelsTransition.FromTransition();
            inGamePanelButton.enabled = true;
            //fadingManager.ShowPanel(backgroundCircle, fadeDuration);
        }
    
        private void ShowRestartPanel()
        {
            mainPanelsTransition.ToTransition();
            //backgroundCircleAnimator.SetTrigger(Animations.FadeOutTrigger);
            OnRestartScreenOpened?.Invoke();
            //fadingManager.HidePanel(backgroundCircle, fadeDuration);
        }

        #region ForButtons
        
        public void ShowBestScoresPanel()
        {
            bestScoresPanelTransition.ToTransition();
        }
        
        public void HideBestScoresPanel()
        {
            bestScoresPanelTransition.ToTransition();
        }
        
        #endregion
    }
}
