using System;
using Common.CommonScripts;
using Common.CommonScripts.Constants;
using Data.TakePhotoController;
using Managers.Player_Handler;
using TMPro;
using UI.Transitions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Managers.Menu_Manager
{
    public class MenuManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TakePhotoController takePhotoController;
        [Header("Player Name Fields")]
        [SerializeField] private TMP_InputField nicknameInputField;
        [SerializeField] private TextMeshProUGUI displayedPlayerName;
        [Header("Displayed Scores Fields")]
        [SerializeField] private TextMeshProUGUI lastScoreText;
        [SerializeField] private TextMeshProUGUI bestScoreText;
        [Header("Transitions")]
        [SerializeField] private ThirdPointTransition setAvatarPanelTransition;
        
        private PlayerHandler _playerHandler;

        private const string LastScoreDisplayedText = "Last score: ";
        private const string BestScoreDisplayedText = "Best score: ";
        
        [Inject]
        private void Construct(PlayerHandler playerHandler)
        {
            _playerHandler = playerHandler ? playerHandler : throw new ArgumentNullException(nameof(playerHandler));
        }

        private void Start()
        {
            TryToLoadLastAndBestScore();
        }

        #region ButtonLinksMethod

        public void StartGame()
        {
            SceneManager.LoadScene(Scenes.GamePlayScene);
        }

        public void OpenSetAvatarPanel()
        {
            setAvatarPanelTransition.ToFirstTransition();
        }
    
        public void CloseSetAvatarPanel()
        {
            setAvatarPanelTransition.FromFirstTransition();
        }

        public void TakePhotoFromCamera()
        {
            takePhotoController.SetPhotoFromCamera();
        }
    
        public void TakePhotoFromFile()
        {
            takePhotoController.SetPhotoFromCamera();
        }
    
        public void SetDefaultAvatar()
        {
            takePhotoController.SetDefaultAvatar();
        }

        public void ChangeName()
        {
            SetNickname(nicknameInputField.text);
        }
        #endregion

        private void SetNickname(string name)
        {
            _playerHandler.SetCurrentPlayerName(name);
            displayedPlayerName.text = _playerHandler.CurrentPlayerName;
            PlayerPrefs.SetString(PlayerPreferencesNaming.PlayerName, name);
        }

        private void TryToLoadLastAndBestScore()
        {
            lastScoreText.text = LastScoreDisplayedText + PlayerPrefs.GetInt(PlayerPreferencesNaming.LastScore);
            bestScoreText.text = BestScoreDisplayedText + PlayerPrefs.GetInt(PlayerPreferencesNaming.HighScore);
        }
    }
}
