using System;
using Common.CommonScripts;
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
        [Header("Transitions")]
        [SerializeField] private TwoPointTransition setAvatarPanelTransition;
        
        private PlayerHandler _playerHandler;
        
        [Inject]
        private void Construct(PlayerHandler playerHandler)
        {
            _playerHandler = playerHandler ? playerHandler : throw new ArgumentNullException(nameof(playerHandler));
        }

        #region ButtonLinksMethod

        public void StartGame()
        {
            SceneManager.LoadScene(Scenes.GamePlayScene);
        }

        public void OpenSetAvatarPanel()
        {
            setAvatarPanelTransition.ToTransition();
        }
    
        public void CloseSetAvatarPanel()
        {
            setAvatarPanelTransition.FromTransition();
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
        }
    }
}
