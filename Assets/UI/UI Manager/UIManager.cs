using System;
using Common.CommonScripts.Constants;
using Managers.Game_Manager;
using SFX.Sound_Manager;
using UI.Scale_Manager;
using UI.Transitions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.UI_Manager
{
    public class UIManager : MonoBehaviour
    {
        public static event Action OnRestartScreenOpened;
        public static event Action OnGameScreenOpened;
        
        [Header("Managers")] 
        [SerializeField] private GameManager gameManager;
        [SerializeField] private SoundManager soundManager;
        [Header("Panels")] 
        [SerializeField] private GameObject inGamePanel;
        [SerializeField] private GameObject restartPanel;
        [SerializeField] private Button inGamePanelButton;
        [Header("Background Circle Fader")]
        [SerializeField] private SpriteFader backgroundCircleSpriteFader;
        [Header("Transition")]
        [SerializeField] private ThirdPointTransition panelsTransition;
        
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
           
        }
        
        public void RestartGame()
        {
            gameManager.RestartGame();
        }
        
        private void ShowInGamePanel()
        {
            panelsTransition.FromFirstTransition();
            inGamePanelButton.enabled = true;
            
            backgroundCircleSpriteFader.FadeSpriteIn();
            OnGameScreenOpened?.Invoke();
        }
    
        private void ShowRestartPanelFromGame()
        {
            panelsTransition.ToFirstTransition();
            OnRestartScreenOpened?.Invoke();
            
            backgroundCircleSpriteFader.FadeSpriteOut();
        }
        
        public void ShowRestartPanelFromHighScore()
        {
            panelsTransition.FromSecondTransition();
            OnRestartScreenOpened?.Invoke();
        }
        
        public void ShowHighScorePanelFromRestart()
        {
            panelsTransition.ToSecondTransition();
        }

        public void ToggleVolume()
        {
            soundManager.ToggleVolume();
        }

        public void LoadMenuScene()
        {
            SceneManager.LoadScene(Scenes.MenuScene);
        }

        public void ToggleFullscreen(bool state)
        {
            Screen.fullScreen = state;
        }
    }
}
