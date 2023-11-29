using System;
using Main_Controller;
using Managers.Player_Handler;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Managers.Game_Manager
{
    public class GameManager : MonoBehaviour
    {
        public static event Action OnTouch;
        public static event Action OnStart;
        public static event Action OnRestart;
        public static event Action OnScoreIncreased;
        public static event Action OnBestScoreIncreased;
        
        private InputManager _inputManager;
        private InputController.PlayerInteractionsActions _playerInteractions;
        
        private PlayerHandler _playerHandler;
        
        public int Scores { get; private set; }
        public int BestScore { get; private set; }
    
        [Inject]
        private void Construct(InputManager inputManager, PlayerHandler playerHandler)
        {
            _inputManager = inputManager ? inputManager : throw new ArgumentNullException(nameof(inputManager));
            _playerHandler = playerHandler ? playerHandler : throw new ArgumentNullException(nameof(playerHandler));
        }

        private void Start()
        {
            _playerInteractions = _inputManager.PlayerInteractions;

            _playerInteractions.Touch.started += OnPlayerTouch;
        }

        private void OnEnable()
        {
            PointController.OnPointPicked += IncreaseScore;

            PlayerBallCollisionController.OnPlayerCircleDestroyed += SetBestScore;
        }

        private void OnDisable()
        {
            PointController.OnPointPicked -= IncreaseScore;
            
            PlayerBallCollisionController.OnPlayerCircleDestroyed -= SetBestScore;
        }

        public void StartGame()
        {
            OnStart?.Invoke();
        }

        public void RestartGame()
        {
            OnRestart?.Invoke();
        }

        public string GetCurrentPlayerName()
        {
            return _playerHandler.CurrentPlayerName;
        }
        
        private void IncreaseScore()
        {
            Scores += 1;
            OnScoreIncreased?.Invoke();
        }
        
        private void SetBestScore()
        {
            if (Scores <= BestScore) return;
            
            BestScore = Scores;
            
            OnBestScoreIncreased?.Invoke();
        }
        
        private void OnPlayerTouch(InputAction.CallbackContext context)
        {
            OnTouch?.Invoke();
        }
    }
    
}