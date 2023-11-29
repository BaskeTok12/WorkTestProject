using System;
using Main_Controller.States;
using Managers.Game_Manager;
using UnityEngine;

namespace Main_Controller
{
    public class PlayerBallController : MonoBehaviour
    {
        public static event Action OnDirectionChanged;
        
        private readonly Vector2 _defaultPlayerBallPosition = new Vector2(0, 1);

        [Header("Player Ball Prefab")] 
        [SerializeField] private GameObject playerBallPrefab;
        [Header("Points Transform")]
        [SerializeField] private Transform centerPointPosition;
        [SerializeField] private Transform playerBallPosition;
        [Header("Parameters")] 
        [SerializeField] private float rotationSpeed;

        private bool _isCanMove;

        private PlayerBallStates _playerBallState;
        
        private void OnEnable()
        {
            GameManager.OnStart += OnPlayerCreated;
            GameManager.OnTouch += TryToChangeDirection;

            PlayerBallCollisionController.OnPlayerCircleDestroyed += OnPlayerDestroyed;
        }

        private void OnDisable()
        {
            GameManager.OnStart -= OnPlayerCreated;
            GameManager.OnTouch -= TryToChangeDirection;
            
            PlayerBallCollisionController.OnPlayerCircleDestroyed -= OnPlayerDestroyed;
        }

        private void Update()
        {
            if (_playerBallState == PlayerBallStates.Active)
            {
                if (_isCanMove)
                {
                    OrbitAroundPoint();
                }
            }
        }
        
        private void OrbitAroundPoint()
        {
            playerBallPosition.RotateAround(centerPointPosition.position, Vector3.forward, rotationSpeed);
        }

        private void TryToChangeDirection()
        {
            if (_playerBallState == PlayerBallStates.Destroyed) return;
            
            rotationSpeed = -rotationSpeed;
            OnDirectionChanged?.Invoke();
        }
    
        private void OnPlayerCreated()
        {
            InstantiateNewBall();
            _isCanMove = true;
            _playerBallState = PlayerBallStates.Active;
        }
        
        private void OnPlayerDestroyed()
        {
            _isCanMove = false;
            _playerBallState = PlayerBallStates.Destroyed;
        }

        private void InstantiateNewBall()
        {
            var newBall = Instantiate(playerBallPrefab, transform, true);
            
            newBall.transform.position = _defaultPlayerBallPosition;

            playerBallPosition = newBall.transform;
        }
    }
}